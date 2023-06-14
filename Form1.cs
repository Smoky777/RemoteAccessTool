using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
                    ServerStop();
                    LabelInfo.Text = "Serveur arrêté.";
                    BtnStart.Text = "START";
                    isServerRunning = false;
                }
            }
            catch(Exception ex)
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

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                MessageBox.Show("Client : " + message);

                byte[] response = Encoding.ASCII.GetBytes("Message reçu par le serveur.");
                stream.Write(response, 0, response.Length);
            }

            client.Close();
            MessageBox.Show("Connexion client fermée");
            RemoveClient(client);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ServerStop()
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
            serverThread.Join();
            serverListener.Stop();
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
            //item.SubItems.Add(GetOperatingSystemCountry());

            AddItemToListConnection(item);
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

        private string GetCountryNameFromCode(string countryCode)
        {
                Dictionary<string, string> countryCodes = new Dictionary<string, string>
                {
                    { "AF", "Afghanistan" },
                    { "AX", "Åland Islands" },
                    { "AL", "Albania" },
                    { "DZ", "Algeria" },
                    { "AS", "American Samoa" },
                    { "AD", "Andorra" },
                    { "AO", "Angola" },
                    { "AI", "Anguilla" },
                    { "AQ", "Antarctica" },
                    { "AG", "Antigua and Barbuda" },
                    { "AR", "Argentina" },
                    { "AM", "Armenia" },
                    { "AW", "Aruba" },
                    { "AU", "Australia" },
                    { "AT", "Austria" },
                    { "AZ", "Azerbaijan" },
                    { "BS", "Bahamas" },
                    { "BH", "Bahrain" },
                    { "BD", "Bangladesh" },
                    { "BB", "Barbados" },
                    { "BY", "Belarus" },
                    { "BE", "Belgium" },
                    { "BZ", "Belize" },
                    { "BJ", "Benin" },
                    { "BM", "Bermuda" },
                    { "BT", "Bhutan" },
                    { "BO", "Bolivia (Plurinational State of)" },
                    { "BQ", "Bonaire, Sint Eustatius and Saba" },
                    { "BA", "Bosnia and Herzegovina" },
                    { "BW", "Botswana" },
                    { "BV", "Bouvet Island" },
                    { "BR", "Brazil" },
                    { "IO", "British Indian Ocean Territory" },
                    { "BN", "Brunei Darussalam" },
                    { "BG", "Bulgaria" },
                    { "BF", "Burkina Faso" },
                    { "BI", "Burundi" },
                    { "CV", "Cabo Verde" },
                    { "KH", "Cambodia" },
                    { "CM", "Cameroon" },
                    { "CA", "Canada" },
                    { "KY", "Cayman Islands" },
                    { "CF", "Central African Republic" },
                    { "TD", "Chad" },
                    { "CL", "Chile" },
                    { "CN", "China" },
                    { "CX", "Christmas Island" },
                    { "CC", "Cocos (Keeling) Islands" },
                    { "CO", "Colombia" },
                    { "KM", "Comoros" },
                    { "CG", "Congo" },
                    { "CD", "Congo (Democratic Republic of the)" },
                    { "CK", "Cook Islands" },
                    { "CR", "Costa Rica" },
                    { "CI", "Côte d'Ivoire" },
                    { "HR", "Croatia" },
                    { "CU", "Cuba" },
                    { "CW", "Curaçao" },
                    { "CY", "Cyprus" },
                    { "CZ", "Czech Republic" },
                    { "DK", "Denmark" },
                    { "DJ", "Djibouti" },
                    { "DM", "Dominica" },
                    { "DO", "Dominican Republic" },
                    { "EC", "Ecuador" },
                    { "EG", "Egypt" },
                    { "SV", "El Salvador" },
                    { "GQ", "Equatorial Guinea" },
                    { "ER", "Eritrea" },
                    { "EE", "Estonia" },
                    { "ET", "Ethiopia" },
                    { "FK", "Falkland Islands (Malvinas)" },
                    { "FO", "Faroe Islands" },
                    { "FJ", "Fiji" },
                    { "FI", "Finland" },
                    { "FR", "France" },
                    { "GF", "French Guiana" },
                    { "PF", "French Polynesia" },
                    { "TF", "French Southern Territories" },
                    { "GA", "Gabon" },
                    { "GM", "Gambia" },
                    { "GE", "Georgia" },
                    { "DE", "Germany" },
                    { "GH", "Ghana" },
                    { "GI", "Gibraltar" },
                    { "GR", "Greece" },
                    { "GL", "Greenland" },
                    { "GD", "Grenada" },
                    { "GP", "Guadeloupe" },
                    { "GU", "Guam" },
                    { "GT", "Guatemala" },
                    { "GG", "Guernsey" },
                    { "GN", "Guinea" },
                    { "GW", "Guinea-Bissau" },
                    { "GY", "Guyana" },
                    { "HT", "Haiti" },
                    { "HM", "Heard Island and McDonald Islands" },
                    { "VA", "Holy See" },
                    { "HN", "Honduras" },
                    { "HK", "Hong Kong" },
                    { "HU", "Hungary" },
                    { "IS", "Iceland" },
                    { "IN", "India" },
                    { "ID", "Indonesia" },
                    { "IR", "Iran (Islamic Republic of)" },
                    { "IQ", "Iraq" },
                    { "IE", "Ireland" },
                    { "IM", "Isle of Man" },
                    { "IL", "Israel" },
                    { "IT", "Italy" },
                    { "JM", "Jamaica" },
                    { "JP", "Japan" },
                    { "JE", "Jersey" },
                    { "JO", "Jordan" },
                    { "KZ", "Kazakhstan" },
                    { "KE", "Kenya" },
                    { "KI", "Kiribati" },
                    { "KP", "Korea (Democratic People's Republic of)" },
                    { "KR", "Korea (Republic of)" },
                    { "KW", "Kuwait" },
                    { "KG", "Kyrgyzstan" },
                    { "LA", "Lao People's Democratic Republic" },
                    { "LV", "Latvia" },
                    { "LB", "Lebanon" },
                    { "LS", "Lesotho" },
                    { "LR", "Liberia" },
                    { "LY", "Libya" },
                    { "LI", "Liechtenstein" },
                    { "LT", "Lithuania" },
                    { "LU", "Luxembourg" },
                    { "MO", "Macao" },
                    { "MK", "North Macedonia" },
                    { "MG", "Madagascar" },
                    { "MW", "Malawi" },
                    { "MY", "Malaysia" },
                    { "MV", "Maldives" },
                    { "ML", "Mali" },
                    { "MT", "Malta" },
                    { "MH", "Marshall Islands" },
                    { "MQ", "Martinique" },
                    { "MR", "Mauritania" },
                    { "MU", "Mauritius" },
                    { "YT", "Mayotte" },
                    { "MX", "Mexico" },
                    { "FM", "Micronesia (Federated States of)" },
                    { "MD", "Moldova (Republic of)" },
                    { "MC", "Monaco" },
                    { "MN", "Mongolia" },
                    { "ME", "Montenegro" },
                    { "MS", "Montserrat" },
                    { "MA", "Morocco" },
                    { "MZ", "Mozambique" },
                    { "MM", "Myanmar" },
                    { "NA", "Namibia" },
                    { "NR", "Nauru" },
                    { "NP", "Nepal" },
                    { "NL", "Netherlands" },
                    { "NC", "New Caledonia" },
                    { "NZ", "New Zealand" },
                    { "NI", "Nicaragua" },
                    { "NE", "Niger" },
                    { "NG", "Nigeria" },
                    { "NU", "Niue" },
                    { "NF", "Norfolk Island" },
                    { "MP", "Northern Mariana Islands" },
                    { "NO", "Norway" },
                    { "OM", "Oman" },
                    { "PK", "Pakistan" },
                    { "PW", "Palau" },
                    { "PS", "Palestine, State of" },
                    { "PA", "Panama" },
                    { "PG", "Papua New Guinea" },
                    { "PY", "Paraguay" },
                    { "PE", "Peru" },
                    { "PH", "Philippines" },
                    { "PN", "Pitcairn" },
                    { "PL", "Poland" },
                    { "PT", "Portugal" },
                    { "PR", "Puerto Rico" },
                    { "QA", "Qatar" },
                    { "RE", "Réunion" },
                    { "RO", "Romania" },
                    { "RU", "Russian Federation" },
                    { "RW", "Rwanda" },
                    { "BL", "Saint Barthélemy" },
                    { "SH", "Saint Helena, Ascension and Tristan da Cunha" },
                    { "KN", "Saint Kitts and Nevis" },
                    { "LC", "Saint Lucia" },
                    { "MF", "Saint Martin (French part)" },
                    { "PM", "Saint Pierre and Miquelon" },
                    { "VC", "Saint Vincent and the Grenadines" },
                    { "WS", "Samoa" },
                    { "SM", "San Marino" },
                    { "ST", "Sao Tome and Principe" },
                    { "SA", "Saudi Arabia" },
                    { "SN", "Senegal" },
                    { "RS", "Serbia" },
                    { "SC", "Seychelles" },
                    { "SL", "Sierra Leone" },
                    { "SG", "Singapore" },
                    { "SX", "Sint Maarten (Dutch part)" },
                    { "SK", "Slovakia" },
                    { "SI", "Slovenia" },
                    { "SB", "Solomon Islands" },
                    { "SO", "Somalia" },
                    { "ZA", "South Africa" },
                    { "GS", "South Georgia and the South Sandwich Islands" },
                    { "SS", "South Sudan" },
                    { "ES", "Spain" },
                    { "LK", "Sri Lanka" },
                    { "SD", "Sudan" },
                    { "SR", "Suriname" },
                    { "SJ", "Svalbard and Jan Mayen" },
                    { "SZ", "Eswatini" },
                    { "SE", "Sweden" },
                    { "CH", "Switzerland" },
                    { "SY", "Syrian Arab Republic" },
                    { "TW", "Taiwan" },
                    { "TJ", "Tajikistan" },
                    { "TZ", "Tanzania, United Republic of" },
                    { "TH", "Thailand" },
                    { "TL", "Timor-Leste" },
                    { "TG", "Togo" },
                    { "TK", "Tokelau" },
                    { "TO", "Tonga" },
                    { "TT", "Trinidad and Tobago" },
                    { "TN", "Tunisia" },
                    { "TR", "Turkey" },
                    { "TM", "Turkmenistan" },
                    { "TC", "Turks and Caicos Islands" },
                    { "TV", "Tuvalu" },
                    { "UG", "Uganda" },
                    { "UA", "Ukraine" },
                    { "AE", "United Arab Emirates" },
                    { "GB", "United Kingdom of Great Britain and Northern Ireland" },
                    { "US", "United States of America" },
                    { "UM", "United States Minor Outlying Islands" },
                    { "UY", "Uruguay" },
                    { "UZ", "Uzbekistan" },
                    { "VU", "Vanuatu" },
                    { "VE", "Venezuela (Bolivarian Republic of)" },
                    { "VN", "Viet Nam" },
                    { "VG", "Virgin Islands (British)" },
                    { "VI", "Virgin Islands (U.S.)" },
                    { "WF", "Wallis and Futuna" },
                    { "EH", "Western Sahara" },
                    { "YE", "Yemen" },
                    { "ZM", "Zambia" },
                    { "ZW", "Zimbabwe" }
                };

                if (countryCodes.ContainsKey(countryCode))
                {
                    return countryCodes[countryCode];
                }

                return "Unknown";

        }

        private string GetOperatingSystemCountry()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");

            foreach (ManagementObject ms in mos.Get())
            {
                string countryCode = ms["CountryCode"].ToString();

                // Utilisez ici une méthode de conversion pour obtenir le nom du pays en fonction du code de pays
                string countryName = GetCountryNameFromCode(countryCode);

                return countryName;
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

    }
    
}


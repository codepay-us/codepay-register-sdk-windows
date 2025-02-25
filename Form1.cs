﻿using System;
using System.Windows.Forms;
using System.Text.Json;
using WebSocketSharp;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using WebSocketSharp.Server;
using Makaretu.Dns;
using Tmds.MDns;
namespace ECRWlanDemo
{
    public partial class Form1 : Form
    {
        //client websocket
        private WebSocket _clientWebSocket;

        //pair server websocket
        private WebSocketServer _serverWebSocket;

        //mdns register service
        private ServiceDiscovery _serviceDiscovery;

        // mdns listener
        private ServiceBrowser _serviceBrowser;

        private static PairedDataSave _pairedDataSave = new PairedDataSave();

        // ECR System Name,This name will be displayed on the terminal.
        private static String ECR_NAME = "My ECR";

        //ECR system unique ID
        private static String MAC_ADDRESS = "123456";

        //client type
        private static String REMOTE_CLIENT_TYPE = "_ecr-hub-client._tcp.";

        // server type
        public static String REMOTE_SERVER_TYPE = "_ecr-hub-server._tcp";

        //default port,you can set a port that is not commonly used
        private static ushort PORT = 35779;

        //The paired devices can be stored in a file, and the next ECR system restart can retrieve the paired information and directly connect to the device's server
        private static DeviceData _pairedData = null;

        private static TextBox _inputIpTextBox = null;


        public Form1()
        {
            InitializeComponent();
            _inputIpTextBox = textBox1;
            _pairedData = _pairedDataSave.LoadPairedData();
            registerMdnsListener();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check whether _clientWebSocket has been initialized and is in a connected state
            if (null != _clientWebSocket && _clientWebSocket.ReadyState == WebSocketState.Open)
            {
                // If the WebSocket connection is open, output 'Connected!!' and exit the current method
                Console.WriteLine("Connected!!");
                return;
            }

            if(null == _pairedData)
            {
                _pairedData = _pairedDataSave.LoadPairedData();
            }

            string input = ((TextBox)this.Controls["textBox1"]).Text;
            if ((null == input || input == "")&& null == _pairedData)
            {
                Console.WriteLine("Please input ip address or wait device paired");
                return;
            }
            if(null == input || "" == input)
            {
                input = _pairedData.IpAddress+":"+ _pairedData.Port;
            }
            var _serverUrl = "ws://" + input;

            // Initiate the WebSocket client's connection process
            connectServer(_serverUrl);

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("connect status:" + _clientWebSocket.ReadyState);
            var bizData = new BizData
            {
                MerchantOrderNo = "123456",
                PayScenario = "SWIPE_CARD",
                OrderAmount = "200",
                //If the transaction does not include tip, 0 or an empty string can be used
                TipAmount = "20",
                TransType = "1"
            };
            var data = new ECRHubMessageData
            {
                Topic = Constants.PAYMENT_TOPIC,
                Appid = "wz6012822ca2f1as78",
                Bizdata = bizData
            };
            // If _clientWebSocket is null or its state is not connected (Open), attempt to connect
            if (null == _clientWebSocket || _clientWebSocket.ReadyState != WebSocketState.Open)
            {
                _clientWebSocket.Connect();

                // Wait for 1 second to give the connection some time
                await Task.Delay(1000);

                // If the connection is successful and the WebSocket state is Open, proceed with the payment operation
                if (_clientWebSocket.ReadyState == WebSocketState.Open)
                {
                    startTransactions(data);
                }
            }
            else
            {
                // If the WebSocket is already connected, directly proceed with the payment operation
                startTransactions(data);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            disconnect();
        }

        /**
         * The flow of the pairing logic is as follows:
             1. start a paired server at the ECR.
             2. Register the IP address and other information of the ECR in the LAN so that the ECR can be found in the payment terminal
             3. Listen to the payment terminal server at the ECR.
             4. Open the ECR setting interface of the payment terminal, and you can find the ECR that can be paired, and then click Pairing.
             5. The ECR will receive the pairing information, the ECR confirms the pairing, and the pairing process between the ECR and the payment terminal is completed. You can get the ip address and port of the payment terminal server from the paired information. Then you can connect to the server of the payment terminal.
             6. When the ip address or port of the payment terminal is found to be changed, the listening of ECR can receive the information of the change and judge whether it is the paired device from the information. If it has been paired, it updates the paired ip address and port, and then reconnects to the server of the payment terminal.
         */
        private void button5_Click(object sender, EventArgs e)
        {
            if (null == _serverWebSocket)
            {
                //get local WLAN ip address
                var ipAddress = getLoacalIpAddress();
                if (ipAddress == "")
                {
                    return;
                }
                var url = "ws://" + ipAddress + ":" + PORT;
                Console.WriteLine("url:" + url);
                _serverWebSocket = new WebSocketServer(url);
            }

            if (!_serverWebSocket.IsListening)
            {
                _serverWebSocket.AddWebSocketService<EchoService>("/");
                // start pair Server
                _serverWebSocket.Start();
                Console.WriteLine("WebSocket server started");
                registerMDNSService();
                registerMdnsListener();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(null !=_serverWebSocket&&_serverWebSocket.IsListening)
            {
                _serverWebSocket.Stop();
                _serverWebSocket = null;
            }
            unRegisterMDNSService();
            unRegisterMdnsListener();
        }

        // UnRegister ECR listener terminal
        private void  unRegisterMdnsListener()
        {
            if(null !=_serviceBrowser)
            {
                _serviceBrowser.StopBrowse();
                _serviceBrowser = null;
            }
        }


        // Register ECR listener terminal
        private void registerMdnsListener()
        {
            if(null ==_serviceBrowser)
            {
                _serviceBrowser = new ServiceBrowser();
            }
            _serviceBrowser.ServiceAdded += onServiceAdded;
            _serviceBrowser.ServiceRemoved += onServiceRemoved;
            _serviceBrowser.ServiceChanged += onServiceChanged;

            Console.WriteLine("Browsing for type: {0}", REMOTE_SERVER_TYPE);
            _serviceBrowser.StartBrowse("_ecr-hub-server._tcp");
            Console.ReadLine();
        }

        void onServiceChanged(object sender, ServiceAnnouncementEventArgs e)
        {
            UpdateServer(e.Announcement);
        }

         void onServiceRemoved(object sender, ServiceAnnouncementEventArgs e)
        {
            Console.WriteLine("on service removed");
        }

         void onServiceAdded(object sender, ServiceAnnouncementEventArgs e)
        {
            UpdateServer(e.Announcement);
        }

        void UpdateServer(ServiceAnnouncement service)
        {
            Console.WriteLine("{0}' on {1}", service.Instance, service.NetworkInterface.Name);
            Console.WriteLine("\tHost: {0} ({1})", service.Hostname, string.Join(", ", service.Addresses));
            Console.WriteLine("\tPort: {0}", service.Port);
            var info = string.Join(", ", service.Txt);
            Console.WriteLine("\tTxt : [{0}]", info);
            DeviceData data = JsonSerializer.Deserialize<DeviceData>(info);
            if (null != _pairedData && _pairedData.MacAddress == data.MacAddress)
            {
                if (data.IpAddress != _pairedData.IpAddress || data.Port != _pairedData.Port)
                {
                    _pairedData.MacAddress = data.MacAddress;
                    _pairedData.IpAddress = data.IpAddress;
                    _pairedData.Port = data.Port;
                    _pairedDataSave.SavePairedData(_pairedData);
                    var url = "ws://" + _pairedData.IpAddress + ":" + _pairedData.Port;
                    connectServer(url);
                }
            }
        }

        //Resgiter local ip into wlan
        private void registerMDNSService()
        {
            if (null == _serviceDiscovery)
            {
                _serviceDiscovery = new ServiceDiscovery();
            }
            var profile = new ServiceProfile(ECR_NAME, REMOTE_CLIENT_TYPE, PORT);
            profile.AddProperty("mac_address", MAC_ADDRESS);
            profile.AddProperty("ip_address", getLoacalIpAddress()+":"+PORT);
            _serviceDiscovery.AnswersContainsAdditionalRecords = true;
            _serviceDiscovery.Advertise(profile);
            _serviceDiscovery.Announce(profile);
            Console.WriteLine("register success");
        }

        // UnRegister local ip
        private void unRegisterMDNSService()
        {
            if (null != _serviceDiscovery)
            {
                _serviceDiscovery.Unadvertise();
                _serviceDiscovery.Dispose();
                _serviceDiscovery = null;
            }
        }

        //The ECR receives requests to pair and unpair payment terminals. the ECR confirms the pairing and gets the ip address and port of the payment terminal server.
        public class EchoService : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
               Console.WriteLine("MessageEventArgs" + e.Data);
               ECRHubMessageData data = JsonSerializer.Deserialize<ECRHubMessageData>(e.Data);
                Console.WriteLine("topic" + data.Topic);
                if (data.Topic == Constants.ECR_HUB_TOPIC_PAIR)
                {
                    DialogResult result = MessageBox.Show("Confirm pairing this device:" + data.DeviceData.DeviceName, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    // confirm pair
                    if (result == DialogResult.OK)
                    {
                        Console.WriteLine("confirm pair");
                        _pairedData = data.DeviceData;
                        _pairedDataSave.SavePairedData(_pairedData);
                        data.ResponseCode = Constants.SUCCESS_STATUS;
                        string message = JsonSerializer.Serialize(data);
                        Console.WriteLine("Send Data JSON: " + message);
                        Send(message);
                    }
                    //cancel pair
                    else if (result == DialogResult.Cancel)
                    {
                        Console.WriteLine("cancel pair");
                        data.ResponseCode = Constants.FAIL_STATUS;
                        string message = JsonSerializer.Serialize(data);
                        Console.WriteLine("Send Data JSON: " + message);
                        Send(message);
                    }

                }
                // unpair request
                else if(data.Topic == Constants.ECR_HUB_TOPIC_UNPAIR)
                {
                    _pairedData = null;
                    _pairedDataSave.SavePairedData(_pairedData);
                    data.ResponseCode = Constants.SUCCESS_STATUS;
                    string message = JsonSerializer.Serialize(data);
                    Console.WriteLine("Send Data JSON: " + message);
                    Send(message);
                }
            }
        }

        /**
        * Get local ip address
        */
        private string getLoacalIpAddress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface network in networkInterfaces)
            {
                if (network.OperationalStatus == OperationalStatus.Up && network.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPInterfaceProperties ipProperties = network.GetIPProperties();
                    foreach (UnicastIPAddressInformation address in ipProperties.UnicastAddresses)
                    {
                        if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var ipAddress = address.Address.ToString();
                            Console.WriteLine($"IP Address: {address.Address}");
                            if (ipAddress.StartsWith("192"))
                            {
                                return ipAddress;
                            }
                        }
                    }
                }
            }
            return "";
        }


        //Starting a transaction request
        private async void startTransactions(ECRHubMessageData data)
        {
            string message = JsonSerializer.Serialize(data);
            Console.WriteLine("Send Data JSON: " + message);
            _clientWebSocket.Send(message);

            // Wait for 500 milliseconds to allow some time for the WebSocket to transmit the message
            await Task.Delay(500);

            // Check if the WebSocket connection is still open
            if (_clientWebSocket.ReadyState != WebSocketState.Open)
            {
                // If the connection is not open, try to reconnect
                _clientWebSocket.Connect();

                // Wait for 1 second to give the connection some time to establish
                await Task.Delay(1000);

                // If the connection is successfully established and the WebSocket state is Open, proceed with the payment
                if (_clientWebSocket.ReadyState == WebSocketState.Open)
                {
                    startTransactions(data);
                }
            }
        }

        // Disconnect the payment terminal server
        private void disconnect()
        {
            // Close WebSocket Connect
            if (null != _clientWebSocket)
            {
                _clientWebSocket.Close();
                _clientWebSocket = null;
            }
            Console.WriteLine("WebSocket Connect Closed!");
        }

        // Connect the payment terminal server
        private void connectServer(String ip)
        {
            if (null == _clientWebSocket)
            {
                _clientWebSocket = new WebSocket(ip);

            }else
            {
                disconnect();
                _clientWebSocket = new WebSocket(ip);
            }
            // Set up an event handler
            _clientWebSocket.OnOpen += (sender, e) =>
            {
                label4.Text = "Payment terminal server connected";
                button3.Visible = true;
                button4.Visible = true;
                Console.WriteLine("WebSocket opened.");
            };
            _clientWebSocket.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Received: " + e.Data);
            };
            _clientWebSocket.OnError += (sender, e) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    label4.Text = "Payment terminal server not connected";
                    button3.Visible = false;
                    button4.Visible = false;
                }));
                Console.WriteLine("Error: " + e.Message);
            };
            _clientWebSocket.OnClose += (sender, e) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    label4.Text = "Payment terminal server not connected";
                    button3.Visible = false;
                    button4.Visible = false;
                }));
              
                Console.WriteLine("WebSocket closed.");
            };
            try
            {
                // Connect to the WebSocket server
                _clientWebSocket.Connect();
                Console.WriteLine("Connected to WebSocket server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to WebSocket server: " + ex.Message);
            }

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("connect status:" + _clientWebSocket.ReadyState);
            var data = new ECRHubMessageData
            {
                Topic = Constants.CLOSE_TOPIC,
                Appid = "wz6012822ca2f1as78",
            };
            // If _clientWebSocket is null or its state is not connected (Open), attempt to connect
            if (null == _clientWebSocket || _clientWebSocket.ReadyState != WebSocketState.Open)
            {
                _clientWebSocket.Connect();

                // Wait for 1 second to give the connection some time
                await Task.Delay(1000);

                // If the connection is successful and the WebSocket state is Open, proceed with the payment operation
                if (_clientWebSocket.ReadyState == WebSocketState.Open)
                {
                    startTransactions(data);
                }
            }
            else
            {
                // If the WebSocket is already connected, directly proceed with the payment operation
                startTransactions(data);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using WiseShare;

namespace EcrHost_Trans_Demo
{

    public partial class Form1 : Form
    {
        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECR_Init", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECR_Init(int dwConnectionType);

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECRLAN_Connect", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECRLAN_Connect(string pszIP, int dwPort, int dwWaitingSeconds, ref ST_ECR_CONNECTION_CALLBACK pstConnectionCallback);

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECRLAN_Disconnect", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECRLAN_Disconnect();

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECRLAN_isConnected", CallingConvention = CallingConvention.StdCall)]
        extern static int ECRLAN_isConnected(); // return 1 connnect

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECRUSB_Connect", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECRUSB_Connect(ref ST_ECR_CONNECTION_CALLBACK pstConnectionCallback);

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECRUSB_Disconnect", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECRUSB_Disconnect();

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECRUSB_isConnected", CallingConvention = CallingConvention.StdCall)]
        extern static int ECRUSB_isConnected();

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECR_GetTerminalInfo", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECR_GetTerminalInfo(string pszRequestMessage, int dwWaitingSeconds, ref ST_ECR_TRANS_CALLBACK pstTransCallback);


        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECR_DoTransaction", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECR_DoTransaction(string pszRequestMessage, int dwWaitingSeconds, ref ST_ECR_TRANS_CALLBACK pstTransCallback);

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECR_CancelTransaction", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECR_CancelTransaction(string pszRequestMessage, int dwWaitingSeconds);

        [DllImport(@"WiseSdk_EcrHost_Payment_X86.dll", EntryPoint = "ECR_QueryTransaction", CallingConvention = CallingConvention.StdCall)]
        extern static UInt32 ECR_QueryTransaction(string pszRequestMessage, int dwWaitingSeconds, ref ST_ECR_TRANS_CALLBACK pstTransCallback);

        // C header file callback function
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void onWsConnected();

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void onWsDisconnected();

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void onWsError(int dwErrCode, string pszErrMsg);


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void onTransSuccess(string pszResponse);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void onTransError(int dwErrCode, string pszErrMsg);


        public struct ST_ECR_CONNECTION_CALLBACK
        {
            public onWsConnected WsConnected;
            public onWsDisconnected WsDisconnected;
            public onWsError WsError;
        }

        public struct ST_ECR_TRANS_CALLBACK
        {
            public onTransSuccess TransSuccess;
            public onTransError TransError;
        }

        enum ConnectState
        {
            CONNECT = 0,
            DISCONNECT
        }
        private const int TRUE = 1;
        private const int FALSE = 0;
        private const int CONNECTION_TYPE_USB = 1;
        private const int CONNECTION_TYPE_LAN = 2;
        static Form1 Ins = null;
        int _connectionType = CONNECTION_TYPE_LAN;
        private const string IP_Pattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private const string IP_Path = "ip.dat";

        static ST_ECR_CONNECTION_CALLBACK _ecrConnectCallback = new ST_ECR_CONNECTION_CALLBACK();
        static ST_ECR_TRANS_CALLBACK _transCallback = new ST_ECR_TRANS_CALLBACK();

        public Form1()
        {
            InitializeComponent();
            Ins = this;
            _connectionType = CONNECTION_TYPE_LAN;
            UInt32 ret = ECR_Init(_connectionType);

            if (System.IO.File.Exists(IP_Path))
                tbTeminalIp.Text = System.IO.File.ReadAllText(IP_Path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetConnectStateCtrl(ConnectState.DISCONNECT);
        }

        static void WsConnected_Event()
        {
            Logger.Info("The LAN connection is successful.", Form1.Ins.tbLog);
            SetConnectStateCtrl(ConnectState.CONNECT);

            // getinfo获取终端信息
            _transCallback.TransSuccess = new onTransSuccess(TransSuccess_Event);
            _transCallback.TransError = new onTransError(TransError_Event);

            // Construct json getinfo data
            PaymentRequestParams reqParams = new PaymentRequestParams();
            reqParams.topic = Constants.GETINFO_TOPIC;
            reqParams.device_data = new PaymentRequestParams.DeviceData();
            reqParams.device_data.device_name = System.Net.Dns.GetHostName();

            string json = JsonConvert.SerializeObject(reqParams, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            UInt32 ret = ECR_GetTerminalInfo(json, 60, ref _transCallback);
            if (ret == ERR_ECR.ERR_ECR_SUCCESS)
            {
                Logger.Info($"ECR_GetTerminalInfo:{json}", Form1.Ins.tbLog);
            }
            else
            {
                Logger.Info($"ECR_GetTerminalInfo ErrorCode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            }
        }

        static void WsDisconnected_Event()
        {
            Logger.Info("The LAN disconnection is successful.", Form1.Ins.tbLog);
            SetConnectStateCtrl(ConnectState.DISCONNECT);
        }

        static void WsError_Event(int dwErrCode, string pszErrMsg)
        {
            ConnectWsServer();
            Logger.Info($"WsError_Event dwErrCode:{dwErrCode.ToString("X2")}, {pszErrMsg}!", Form1.Ins.tbLog);
        }

        static void UsbConnected_Event()
        {
            Logger.Info("The USB connection is successful.", Form1.Ins.tbLog);
        }

        static void UsbDisconnected_Event()
        {
            Logger.Info("The USB disconnection is successful.", Form1.Ins.tbLog);
        }

        static void UsbError_Event(int dwErrCode, string pszErrMsg)
        {
            Logger.Info($"UsbError_Event dwErrCode:{dwErrCode.ToString("X2")}!", Form1.Ins.tbLog);
        }


        static void TransSuccess_Event(string pszResponse)
        {
            Logger.Info($"TransSuccess_Event success:{pszResponse}", Form1.Ins.tbLog);

            // 处理"topic": "ecrhub.getInfo"
            PaymentRequestParams resParams = JsonConvert.DeserializeObject<PaymentRequestParams>(pszResponse);
            if (resParams.topic == Constants.GETINFO_TOPIC)
            {
                if (resParams.device_data == null)
                {
                    Logger.Info("The device data is null.", Form1.Ins.tbLog);
                    return;
                }
                Form1.Ins.btnConnectState.Invoke(new MethodInvoker(delegate ()
                {
                    Form1.Ins.btnConnectState.Text = resParams.device_data.device_name;
                }));
            }
        }

        static void TransError_Event(int dwErrCode, string pszErrMsg)
        {
            Logger.Info($"TransError_Event dwErrCode:{dwErrCode.ToString("X2")}, {pszErrMsg}!", Form1.Ins.tbLog);
        }

        static UInt32 ConnectWsServer()
        {
            if (TRUE == ECRLAN_isConnected())
                return ERR_ECR.ERR_ECR_SUCCESS;

            _ecrConnectCallback.WsConnected = new onWsConnected(WsConnected_Event);
            _ecrConnectCallback.WsDisconnected = new onWsDisconnected(WsDisconnected_Event);
            _ecrConnectCallback.WsError = new onWsError(WsError_Event);

            string strIp = Form1.Ins.tbTeminalIp.Text;
            UInt32 ret = ECRLAN_Connect(strIp, 35779, 3, ref _ecrConnectCallback);
            return ret;
        }

        static UInt32 ConnectUsb()
        {
            if (TRUE == ECRUSB_isConnected())
            {
                return ERR_ECR.ERR_ECR_SUCCESS;
            }

            _ecrConnectCallback.WsConnected = new onWsConnected(UsbConnected_Event);
            _ecrConnectCallback.WsDisconnected = new onWsDisconnected(UsbDisconnected_Event);
            _ecrConnectCallback.WsError = new onWsError(UsbError_Event);

            UInt32 ret = ECRUSB_Connect(ref _ecrConnectCallback);

            return ret;
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            UInt32 ret = ERR_ECR.ERR_ECR_SUCCESS;
            if (_connectionType == CONNECTION_TYPE_LAN)
            {
                if (!Regex.IsMatch(tbTeminalIp.Text, IP_Pattern))
                {
                    Logger.Info($"The IP format is incorrect", Form1.Ins.tbLog);
                    return;
                }
                ret = ConnectWsServer();
            }  
            else
                ret = ConnectUsb();

            if (ret != ERR_ECR.ERR_ECR_SUCCESS)
            {
                Logger.Info($"btnConnect_Click Errorcode:{ret.ToString("X2")}", Form1.Ins.tbLog);
                return;
            }

            //// getinfo获取终端信息
            //_transCallback.TransSuccess = new onTransSuccess(TransSuccess_Event);
            //_transCallback.TransError = new onTransError(TransError_Event);

            //// Construct json getinfo data
            //PaymentRequestParams reqParams = new PaymentRequestParams();
            //reqParams.topic = Constants.GETINFO_TOPIC;
            //reqParams.device_data = new PaymentRequestParams.DeviceData();
            //reqParams.device_data.device_name = System.Net.Dns.GetHostName();

            //string json = JsonConvert.SerializeObject(reqParams, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //ret = ECR_GetTerminalInfo(json, 60, ref _transCallback);
            //if (ret == ERR_ECR.ERR_ECR_SUCCESS)
            //{
            //    Logger.Info($"ECR_GetTerminalInfo:{json}", Form1.Ins.tbLog);
            //}
            //else
            //{
            //    Logger.Info($"ECR_GetTerminalInfo ErrorCode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            //}
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            UInt32 ret = ERR_ECR.ERR_ECR_SUCCESS;
            if (_connectionType == CONNECTION_TYPE_LAN)
                ret = ECRLAN_Disconnect();
            else
                ret = ECRUSB_Disconnect();

            if (ret != ERR_ECR.ERR_ECR_SUCCESS && e != null)
            {
                Logger.Info($"Disconnect_Click Errorcode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            }
        }

        private void radUsb_CheckedChanged(object sender, EventArgs e)
        {
            if (radUsb.Checked == true)
            {
                Disconnect_Click(null, null);
                _connectionType = CONNECTION_TYPE_USB;
                UInt32 ret = ECR_Init(CONNECTION_TYPE_USB);
                ChangeCtrlVisible(CONNECTION_TYPE_USB);
            }
        }

        private void radWifi_CheckedChanged(object sender, EventArgs e)
        {
            if (radWifi.Checked == true)
            {
                Disconnect_Click(null, null);
                _connectionType = CONNECTION_TYPE_LAN;
                UInt32 ret = ECR_Init(CONNECTION_TYPE_LAN);
                ChangeCtrlVisible(CONNECTION_TYPE_LAN);
            }
        }


        private void ChangeCtrlVisible(int dwConnectionType)
        {
            if (CONNECTION_TYPE_USB == dwConnectionType)
            {
                btnConnectState.Visible = false;
                lbIp.Visible = false;
                tbTeminalIp.Visible = false;
            }
            else
            {
                //btnConnectState.Visible = true;
                lbIp.Visible = true;
                tbTeminalIp.Visible = true;
            }
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOrderNumber.Text))
            {
                MessageBox.Show("Merchant Order No cannot be empty！");
                return;
            }
            if (string.IsNullOrEmpty(tbAmount.Text))
            {
                MessageBox.Show("Amount cannot be empty！");
                return;
            }
            if (string.IsNullOrEmpty(tbAppId.Text))
            {
                MessageBox.Show("App Id cannot be empty！");
                return;
            }

            if (FALSE == ECRUSB_isConnected())
            {
                ConnectUsb();
            }

            // sale
            _transCallback.TransSuccess = new onTransSuccess(TransSuccess_Event);
            _transCallback.TransError = new onTransError(TransError_Event);

            // Construct json sale data
            PaymentRequestParams reqParams = new PaymentRequestParams();
            reqParams.app_id = tbAppId.Text;
            reqParams.topic = Constants.PAYMENT_TOPIC;
            reqParams.request_id = "111111";
            //reqParams.voice_data = new PaymentRequestParams.VoiceData();
            //reqParams.voice_data.content = "CodePay Register Received a new order";
            //reqParams.voice_data.content_locale = "en-US";
            reqParams.biz_data = new PaymentRequestParams.BizData();
            reqParams.biz_data.merchant_order_no = tbOrderNumber.Text + DateTime.Now.ToString("yyyyMMddHHmmss");// "123" + DateTime.Now.ToString("yyyyMMddHHmmss");
            reqParams.biz_data.order_amount = tbAmount.Text;//"11";
            //reqParams.biz_data.on_screen_tip = false;
            //reqParams.biz_data.print_receipt = 0;
            reqParams.biz_data.pay_scenario = "SWIPE_CARD";
            reqParams.biz_data.trans_type = Constants.TRANS_TYPE_SALE;

            string json = JsonConvert.SerializeObject(reqParams, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            UInt32 ret = ECR_DoTransaction(json, 120, ref _transCallback);
            if (ret == ERR_ECR.ERR_ECR_SUCCESS)
            {
                Logger.Info($"Sale:{json}", Form1.Ins.tbLog);
            }
            else
            {
                Logger.Info($"Sale ErrorCode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            }
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOrderNumber.Text))
            {
                MessageBox.Show("Merchant Order No cannot be empty！");
                return;
            }
            if (string.IsNullOrEmpty(tbAmount.Text))
            {
                MessageBox.Show("Amount cannot be empty！");
                return;
            }
            if (string.IsNullOrEmpty(tbAppId.Text))
            {
                MessageBox.Show("App Id cannot be empty！");
                return;
            }

            // refund
            _transCallback.TransSuccess = new onTransSuccess(TransSuccess_Event);
            _transCallback.TransError = new onTransError(TransError_Event);

            // Construct json refund data
            PaymentRequestParams reqParams = new PaymentRequestParams();
            reqParams.orig_merchant_order_no = tbOrderNumber.Text;
            reqParams.order_amount = tbAmount.Text;
            reqParams.app_id = tbAppId.Text;
            reqParams.topic = Constants.PAYMENT_TOPIC;
            reqParams.request_id = "111111";
            //reqParams.voice_data = new PaymentRequestParams.VoiceData();
            //reqParams.voice_data.content = "CodePay Register Received a new order";
            //reqParams.voice_data.content_locale = "en-US";
            reqParams.biz_data = new PaymentRequestParams.BizData();
            reqParams.biz_data.merchant_order_no = tbOrderNumber.Text;//"123" + DateTime.Now.ToString("yyyyMMddHHmmss");
            reqParams.biz_data.order_amount = tbAmount.Text;//"11";
            //reqParams.biz_data.on_screen_tip = false;
            //reqParams.biz_data.print_receipt = 0;
            reqParams.biz_data.pay_scenario = "SWIPE_CARD";
            reqParams.biz_data.trans_type = Constants.TRANS_TYPE_REFUND;

            string json = JsonConvert.SerializeObject(reqParams, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            UInt32 ret = ECR_DoTransaction(json, 60, ref _transCallback);
            if (ret == ERR_ECR.ERR_ECR_SUCCESS)
            {
                Logger.Info($"Refund:{json}", Form1.Ins.tbLog);
            }
            else
            {
                Logger.Info($"Refund ErrorCode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            }
        }

        private void btnQueryTransaction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOrderNumber.Text))
            {
                MessageBox.Show("Merchant Order No cannot be empty！");
                return;
            }
            if (string.IsNullOrEmpty(tbAppId.Text))
            {
                MessageBox.Show("App Id cannot be empty！");
                return;
            }

            // query
            //_transCallback = new ST_ECR_TRANS_CALLBACK();
            _transCallback.TransSuccess = new onTransSuccess(TransSuccess_Event);
            _transCallback.TransError = new onTransError(TransError_Event);

            // Construct json query data
            PaymentRequestParams reqParams = new PaymentRequestParams();
            reqParams.app_id = tbAppId.Text;
            reqParams.topic = Constants.QUERY_TOPIC;
            reqParams.request_id = "111111";

            reqParams.biz_data = new PaymentRequestParams.BizData();
            reqParams.biz_data.merchant_order_no = tbOrderNumber.Text;
            //reqParams.biz_data.orig_merchant_order_no = reqParams.biz_data.merchant_order_no;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            jsetting.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(reqParams, jsetting);
            UInt32 ret = ECR_QueryTransaction(json, 60, ref _transCallback);
            if (ret == ERR_ECR.ERR_ECR_SUCCESS)
            {
                Logger.Info($"Query:{json}", Form1.Ins.tbLog);
            }
            else
            {
                Logger.Info($"Query ErrorCode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            }
        }

        private void btnCancelTransaction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOrderNumber.Text))
            {
                MessageBox.Show("Merchant Order No cannot be empty！");
                return;
            }
            if (string.IsNullOrEmpty(tbAppId.Text))
            {
                MessageBox.Show("App Id cannot be empty！");
                return;
            }

            // 构造json格式交易数据json
            PaymentRequestParams reqParams = new PaymentRequestParams();
            reqParams.app_id = tbAppId.Text;
            reqParams.topic = Constants.CLOSE_TOPIC;
            reqParams.request_id = "111111";
            reqParams.biz_data = new PaymentRequestParams.BizData();
            reqParams.biz_data.merchant_order_no = tbOrderNumber.Text; //"123" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //reqParams.biz_data.orig_merchant_order_no = tbOrderNumber.Text;
            //reqParams.biz_data.confirm_on_terminal = false;
            //reqParams.biz_data.trans_type = Constants.TRANS_TYPE_VOID;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            jsetting.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(reqParams, jsetting);
            UInt32 ret = ECR_CancelTransaction(json, 60);
            if (ret == ERR_ECR.ERR_ECR_SUCCESS)
            {
                Logger.Info($"Cancel:{json}", Form1.Ins.tbLog);
            }
            else
            {
                Logger.Info($"Cancel ErrorCode:{ret.ToString("X2")}", Form1.Ins.tbLog);
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ECRLAN_isConnected() == TRUE)
                ECRLAN_Disconnect();

            System.IO.File.WriteAllText(IP_Path, tbTeminalIp.Text);
        }


        private static void SetConnectStateCtrl(ConnectState connectState)
        {
            if (connectState == ConnectState.CONNECT)
            {
                Form1.Ins.btnConnectState.Invoke(new MethodInvoker(delegate ()
                {
                    Form1.Ins.btnConnectState.BackColor = Color.LimeGreen;
                    Form1.Ins.btnConnectState.Visible = true;
                }));
            }
            else if (connectState == ConnectState.DISCONNECT)
            {
                Form1.Ins.btnConnectState.Invoke(new MethodInvoker(delegate ()
                {
                    //Form1.Ins.btnConnectState.BackColor = Color.Red;
                    Form1.Ins.btnConnectState.Visible = false;
                }));
            }
        }


    }
}

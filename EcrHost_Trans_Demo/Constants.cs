using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrHost_Trans_Demo
{
    public class Constants
    {
        /**
         * pair
         */
        //消息主题
        public static String ECR_HUB_TOPIC_PAIR = "ecrhub.pair";
        /**
         * unpair
         */
        public static String ECR_HUB_TOPIC_UNPAIR = "ecrhub.unpair";
        /**
         * init topic
         */
        public static String INIT_TOPIC = "ecrhub.init";

    /**
     * payment topic
     */
    public static String PAYMENT_TOPIC = "ecrhub.pay.order";

    /**
     * query topic
     */
    public static String QUERY_TOPIC = "ecrhub.pay.query";

    /**
     * close topic
     */
    public static String CLOSE_TOPIC = "ecrhub.pay.close";

    /**
    * close topic
    */
    public static String GETINFO_TOPIC = "ecrhub.getInfo";

        /**
         * heart beat topic
         */
        public static String HEART_BEAT_TOPIC = "ecrhub.heartbeat";

    /**
     * trans type purchase
     */
    public static String TRANS_TYPE_SALE = "1";

    /**
     * trans type void
     */
    public static String TRANS_TYPE_VOID = "2";

    /**
     * trans type refund
     */
    public static String TRANS_TYPE_REFUND = "3";

    /**
     * trans type pre-auth
     */
    public static String TRANS_TYPE_PRE_AUTH = "4";

    /**
     * trans type pre-auth cancel
     */
    public static String TRANS_TYPE_PRE_AUTH_CANCEL = "5";

    /**
     * trans type pre-auth complete
     */
    public static String TRANS_TYPE_PRE_AUTH_COMPLETE = "6";

    /**
     * trans type pre-auth complete cancel
     */
    public static String TRANS_TYPE_PRE_AUTH_COMPLETE_CANCEL = "7";

    /**
     * trans type pre-auth complete refund
     */
    public static String TRANS_TYPE_PRE_AUTH_COMPLETE_REFUND = "8";

    /**
     * trans type cashback
     */
    public static String TRANS_TYPE_CASH_BACK = "11";
    /**
     * BankCard Payment
     */
    public static String BANKCARD_PAY_TYPE = "BANKCARD";
        /**
         * qr C scan B
         */
        public static String QR_C_SCAN_B_PAY_TYPE = "QR_C_SCAN_B";
        /**
         * qr B scan C
         */
        public static String QR_B_SCAN_C = "QR_B_SCAN_C";
        /**
         * ecr hub pair list key
         */
        public static String ECR_HUB_PAIR_LIST_KEY = "ecr_hub_pair_list_key";

        public enum ECRHubType
        {
            USB, WLAN
        }
    }

    public class ERR_ECR
    {
        public const uint ERR_ECR_SUCCESS = 0;                          // Success

        public const uint ERR_ECR_INVALID_PARAMETER = 0xE0030001;       // Invalid parameter
        public const uint ERR_ECR_LENGTH_OUT_RANGE = 0xE0030002;		// The length is out of range
        public const uint ERR_ECR_DATA_CHECK = 0xE0030005;		        // Bad data checksum
        public const uint ERR_ECR_BAD_DATA = 0xE0030006;		        // Error in the data 
        public const uint ERR_ECR_BUFFER_NOT_ENOUGH = 0xE0030007;		// Insufficient buffer size
        public const uint ERR_ECR_TIMEOUT = 0xE0030008;	                // Communication timeout
        public const uint ERR_ECR_READ_DATA = 0xE003000A;		        // Failed to read data
        public const uint ERR_ECR_WRITE_DATA = 0xE003000B;		        // Failed to write data

        public const uint ERR_ECR_USB_CABLE_NOT_CONNECTED = 0xE003000F;	// No terminal connected to ECR via USB cable
        public const uint ERR_ECR_USB_NOT_CONNECTED = 0xE0030010;		// No USB connection has been established
        public const uint ERR_ECR_LAN_NOT_CONNECTED = 0xE0030011;		// No LAN connection has been established
        public const uint ERR_ECR_LAN_SEND_FAILED = 0xE0030012;		// Failed to send data via Wi-Fi

        public const uint ERR_ECR_DEV_UNPAIRED = 0xE0030013;		    // Device unpaired
        public const uint ERR_ECR_JSON_ERROR = 0xE0030014;		        // Json data incorrect
        public const uint ERR_ECR_WAIT_PAIRING = 0xE0030015;		    // It is already in the state of waiting for the pairing request from the terminal
    }
}

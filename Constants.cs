using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECRWlanDemo
{
    internal class Constants
    {
        // pair topic
        public  static String ECR_HUB_TOPIC_PAIR = "ecrhub.pair";

        // unpair topic 
        public  static String ECR_HUB_TOPIC_UNPAIR = "ecrhub.unpair";

        // payment topic
        public static  String PAYMENT_TOPIC = "ecrhub.pay.order";

        //query topic
        public static  String QUERY_TOPIC = "ecrhub.pay.query";

        // close topic
        public static  String CLOSE_TOPIC = "ecrhub.pay.close";

        // success status
        public static String SUCCESS_STATUS = "000";

        // fail status
        public static String FAIL_STATUS = "001";

    }
}

using System.Text.Json.Serialization;

namespace ECRWlanDemo
{
    public class ECRHubMessageData
    {
        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("app_id")]
        public string Appid { get; set; }

        [JsonPropertyName("response_code")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("response_msg")]
        public string ResponseMsg { get; set; }

        [JsonPropertyName("biz_data")]
        public BizData Bizdata { get; set; }
        [JsonPropertyName("device_data")]
        public DeviceData DeviceData { get; set; }
    }

    public class BizData
    {
        [JsonPropertyName("merchant_order_no")]
        public string MerchantOrderNo { get; set; }

        [JsonPropertyName("pay_scenario")]
        public string PayScenario { get; set; }

        [JsonPropertyName("order_amount")]
        public string OrderAmount { get; set; }

        [JsonPropertyName("tip_amount")]
        public string TipAmount { get; set; }

        [JsonPropertyName("trans_type")]
        public string TransType { get; set; }
    }

    public class DeviceData
    {
        [JsonPropertyName("mac_address")]
        public string MacAddress { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }
        [JsonPropertyName("alias_name")]
        public string AliasName { get; set; }
        [JsonPropertyName("ip_address")]
        public string IpAddress { get; set; }
        [JsonPropertyName("port")]
        public string Port { get; set; }
    }
}

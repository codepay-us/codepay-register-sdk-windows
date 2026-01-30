using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrHost_Trans_Demo
{
    public class PaymentRequestParams
    {
        /**
         *  topic
         */
        public String topic;
        /**
         * The App ID assigned by the system to the caller must be passed in non-offline mode.
         */
        public String app_id;
        /**
         * Merchant order number.
         */
        public String merchant_order_no;
        /**
         * Orig merchant order number
         */
        public String orig_merchant_order_no;
        /**
         * Price Currency, compliant with ISO-4217 standard, described with a three-character code
         */
        public String price_currency;
        /**
         * Order amount.  For example, one USD stands for one dollar, not one cent.
         */
        public String order_amount;
        /**
         * Tip amount. This field represents the transaction tip amount. For example, 1 USD stands for one dollar, not one cent.
         * Example: 3.50
         */
        public String tip_amount;
        /**
         * CashBack amount
         */
        public String cash_amount;
        /**
         * trans type
         */
        public String trans_type;
        /**
         * Specify a payment method. This field is mandatory only when "pay_scenario" is set to "SCANQR_PAY" or "BSCANQR_PAY".
         */
        public String pay_method_id;
        /**
         * description info
         */
        public String description;
        /**
         * Callback address for payment notification.
         * Receive payment notifications from the Gateway to call back the server address, and only when the transaction goes through the payment gateway will there be a callback.
         * Example: http://www.abc.com/callback?id=12345
         */
        public String notify_url;
        /**
         * attach info
         */
        public String attach;

        public String request_id;

        public String pay_scenario;

        public String card_type;

        public BizData biz_data;

        public DeviceData device_data;

        public void setCard_type(String card_type)
        {
            this.card_type = card_type;
        }

        public String getCard_type()
        {
            return card_type;
        }

        public void setOrig_merchant_order_no(String orig_merchant_order_no)
        {
            this.orig_merchant_order_no = orig_merchant_order_no;
        }

        public String getOrig_merchant_order_no()
        {
            return orig_merchant_order_no;
        }

        public void setrequest_id(String request_id)
        {
            this.request_id = request_id;
        }

        public String getrequest_id()
        {
            return request_id;
        }

        public void setTopic(String topic)
        {
            this.topic = topic;
        }

        public String getTopic()
        {
            return topic;
        }

        public String getDescription()
        {
            return description;
        }

        public String getAttach()
        {
            return attach;
        }

        public String getTrans_type()
        {
            return trans_type;
        }

        public String getNotify_url()
        {
            return notify_url;
        }

        public String getPay_method_id()
        {
            return pay_method_id;
        }

        public String getApp_id()
        {
            return app_id;
        }

        public String getMerchant_order_no()
        {
            return merchant_order_no;
        }

        public String getOrder_amount()
        {
            return order_amount;
        }

        public String getPrice_currency()
        {
            return price_currency;
        }

        public String getTip_amount()
        {
            return tip_amount;
        }

        public void setApp_id(String app_id)
        {
            this.app_id = app_id;
        }

        public void setMerchant_order_no(String merchant_order_no)
        {
            this.merchant_order_no = merchant_order_no;
        }

        public void setOrder_amount(String order_amount)
        {
            this.order_amount = order_amount;
        }

        public void setPrice_currency(String price_currency)
        {
            this.price_currency = price_currency;
        }

        public void setTip_amount(String tip_amount)
        {
            this.tip_amount = tip_amount;
        }

        public void setDescription(String description)
        {
            this.description = description;
        }

        public void setAttach(String attach)
        {
            this.attach = attach;
        }

        public void setNotify_url(String notify_url)
        {
            this.notify_url = notify_url;
        }

        public String getPay_scenario()
        {
            return pay_scenario;
        }

        public void setPay_scenario(String pay_scenario)
        {
            this.pay_scenario = pay_scenario;
        }

        public void setPay_method_id(String pay_method_id)
        {
            this.pay_method_id = pay_method_id;
        }

        public void setTrans_type(String trans_type)
        {
            this.trans_type = trans_type;
        }

        public String getCash_amount()
        {
            return cash_amount;
        }

        public void setCash_amount(String cash_amount)
        {
            this.cash_amount = cash_amount;
        }


        public class BizData
        {
            public Boolean confirm_on_terminal;

            //public Boolean on_screen_tip;

            public String order_queue_mode;

            public int expires;

            public String trans_no;

            public String merchant_order_no;

            public String orig_merchant_order_no;

            public String pay_scenario;

            public String pay_method_id;

            public String attach;

            public String description;

            public String notify_url;

            public String trans_status;

            public String msg;

            public String push_no;

            public String order_amount;

            public String cashback_amount;

            public String tip_amount;

            public String price_currency;

            public String trans_type;

            //public Boolean limit_length;

            //public Boolean is_auto_settlement;

            //public int print_receipt;

            public String card_type;

            public void setCard_type(String card_type)
            {
                this.card_type = card_type;
            }

            public String getCard_type()
            {
                return card_type;
            }

            //public Boolean isIs_auto_settlement()
            //{
            //    return is_auto_settlement;
            //}

            public Boolean isConfirm_on_terminal()
            {
                return confirm_on_terminal;
            }

            public String getPay_scenario()
            {
                return pay_scenario;
            }

            public void setPay_scenario(String pay_scenario)
            {
                this.pay_scenario = pay_scenario;
            }

            //public void setIs_auto_settlement(Boolean is_auto_settlement)
            //{
            //    this.is_auto_settlement = is_auto_settlement;
            //}

            String token;

            public void setCashback_amount(String cashback_amount)
            {
                this.cashback_amount = cashback_amount;
            }

            public String getCashback_amount()
            {
                return cashback_amount;
            }

            public void setToken(String token)
            {
                this.token = token;
            }

            public String getToken()
            {
                return token;
            }

            //public void setLimit_length(Boolean limit_length)
            //{
            //    this.limit_length = limit_length;
            //}

            //public Boolean getLimit_length()
            //{
            //    return limit_length;
            //}

            public void setPush_no(String push_no)
            {
                this.push_no = push_no;
            }

            public String getPush_no()
            {
                return push_no;
            }

            //public NotifyData getNotify_data()
            //{
            //    if (null == notify_data)
            //    {
            //        notify_data = new NotifyData();
            //    }
            //    return notify_data;
            //}

            //public PrintData getPrint_data()
            //{
            //    if (null == print_data)
            //    {
            //        print_data = new PrintData();
            //    }
            //    return print_data;
            //}

            //public VoiceData getVoice_data()
            //{
            //    if (null == voice_data)
            //    {
            //        voice_data = new VoiceData();
            //    }
            //    return voice_data;
            //}

            public String getOrder_amount()
            {
                return order_amount;
            }

            public String getPrice_currency()
            {
                return price_currency;
            }

            public String getTip_amount()
            {
                return tip_amount;
            }

            public String getTrans_type()
            {
                return trans_type;
            }

            public void setOrder_amount(String order_amount)
            {
                this.order_amount = order_amount;
            }

            public void setPrice_currency(String price_currency)
            {
                this.price_currency = price_currency;
            }

            public void setTip_amount(String tip_amount)
            {
                this.tip_amount = tip_amount;
            }

            public void setTrans_type(String trans_type)
            {
                this.trans_type = trans_type;
            }

            public String getTrans_status()
            {
                return trans_status;
            }

            public int getExpires()
            {
                return expires;
            }

            public String getOrder_queue_mode()
            {
                return order_queue_mode;
            }

            public void setTrans_status(String trans_status)
            {
                this.trans_status = trans_status;
            }

            public void setAttach(String attach)
            {
                this.attach = attach;
            }

            public void setDescription(String description)
            {
                this.description = description;
            }

            public void setMerchant_order_no(String merchant_order_no)
            {
                this.merchant_order_no = merchant_order_no;
            }

            public void setNotify_url(String notify_url)
            {
                this.notify_url = notify_url;
            }

            public void setOrig_merchant_order_no(String orig_merchant_order_no)
            {
                this.orig_merchant_order_no = orig_merchant_order_no;
            }

            public void setPay_method_id(String pay_method_id)
            {
                this.pay_method_id = pay_method_id;
            }

            public void setTrans_no(String trans_no)
            {
                this.trans_no = trans_no;
            }

            public String getAttach()
            {
                return attach;
            }

            public String getDescription()
            {
                return description;
            }

            public String getMerchant_order_no()
            {
                return merchant_order_no;
            }

            public String getNotify_url()
            {
                return notify_url;
            }

            public String getOrig_merchant_order_no()
            {
                return orig_merchant_order_no;
            }

            public String getPay_method_id()
            {
                return pay_method_id;
            }

            //public String getEndAmount()
            //{
            //    try
            //    {
            //        int amount = Integer.parseInt(order_amount);
            //        if (null != order_amount)
            //        {
            //            if (null != tip_amount)
            //            {
            //                amount += Integer.parseInt(tip_amount);
            //            }
            //            if (null != cashback_amount)
            //            {
            //                amount += Integer.parseInt(cashback_amount);
            //            }
            //        }
            //        return "" + amount;
            //    }
            //    catch (Exception e)
            //    {
            //        return order_amount;
            //    }
            //}

            public String getTrans_no()
            {
                return trans_no;
            }

            public void setMsg(String msg)
            {
                this.msg = msg;
            }

            public String getMsg()
            {
                return msg;
            }


            public void setConfirm_on_terminal(Boolean confirm_on_terminal)
            {
                this.confirm_on_terminal = confirm_on_terminal;
            }

            public Boolean getConfirm_on_terminal()
            {
                return confirm_on_terminal;
            }

            //public int getPrint_receipt()
            //{
            //    return print_receipt;
            //}

            //public void setPrint_receipt(int print_receipt)
            //{
            //    this.print_receipt = print_receipt;
            //}

            public void setExpires(int expires)
            {
                this.expires = expires;
            }

            public void setOrder_queue_mode(String order_queue_mode)
            {
                this.order_queue_mode = order_queue_mode;
            }

            //public Boolean isOn_screen_tip()
            //{
            //    return on_screen_tip;
            //}

            //public void setOn_screen_tip(Boolean on_screen_tip)
            //{
            //    this.on_screen_tip = on_screen_tip;
            //}


        }

        public class DeviceData
        {
            /**
             * device mac address
             */
            public String mac_address;
            /**
             * device name
             */
            public String device_name;
            /**
             * device alias name
             */
            public String alias_name;
            /**
             * server ip address
             */
            public String ip_address;
            /**
             * server port number
             */
            public String port;

            public void setAlias_name(String alias_name)
            {
                this.alias_name = alias_name;
            }

            public void setDevice_name(String device_name)
            {
                this.device_name = device_name;
            }

            public void setIp_address(String ip_address)
            {
                this.ip_address = ip_address;
            }

            public void setMac_address(String mac_address)
            {
                this.mac_address = mac_address;
            }

            public void setPort(String port)
            {
                this.port = port;
            }

            public String getAlias_name()
            {
                return alias_name;
            }

            public String getDevice_name()
            {
                return device_name;
            }

            public String getIp_address()
            {
                return ip_address;
            }

            public String getMac_address()
            {
                return mac_address;
            }

            public String getPort()
            {
                return port;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Utilerias
{
    public class JSONOpenPay
    {
        public string Category { get; set; }
        public int Error_Code { get; set; }
        public string Description { get; set; }
        public string HTTP_Code { get; set; }
        public string Request_ID { get; set; }
        public IList<string> Fraud_Rules { get; set; }
    }
}
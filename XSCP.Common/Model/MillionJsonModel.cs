using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSCP.Common.Model
{
    //{"historyBall":{"1":{"code":"4 8 1 1 9","issue":"20170907-0399"},"2":{"code":"9 6 3 0 0","issue":"20170907-0398"},"3":{"code":"4 9 5 5 2","issue":"20170907-0397"},"4":{"code":"5 7 4 6 6","issue":"20170907-0396"},"5":{"code":"2 6 7 8 2","issue":"20170907-0395"}},"lastballsTrans":["\u524d\u4e09\uff1a\u7ec4\u516d","\u4e2d\u4e09\uff1a\u7ec4\u516d","\u540e\u4e09\uff1a\u7ec4\u516d"],"isSuccess":1,"period":"20170907-0400","ball":"9,3,8,6,1"}
    public class MillionJsonModel
    {
        public ListWiner historyBall { get; set; }

        public string period { get; set; }
        public string ball { get; set; }
    }

    public class ListWiner
    {
        [JsonProperty("1")]
        public MillonWinner period1 { get; set; }

        [JsonProperty("2")]
        public MillonWinner period2 { get; set; }

        [JsonProperty("3")]
        public MillonWinner period3 { get; set; }

        [JsonProperty("4")]
        public MillonWinner period4 { get; set; }

        [JsonProperty("5")]
        public MillonWinner period5 { get; set; }
    }

    public class MillonWinner
    {
        public string code { get; set; }
        public string issue { get; set; }
    }
}

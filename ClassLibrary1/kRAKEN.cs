using Dek572.Cls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mCalc.DataSources
{
    public class Kraken
    {
        public string PublicApiKey { get; set; } = "gUvfcrHJk9Ccb6wr/YbJ9syAJqjUl40RKhMokI6ZjNf3OJrMhBWFo+0c";
        public string PrivateApiKey { get; set; } = "v6iROR25ZMNEXrUjskCRPQMdKjW9sIYdxkrsayVjLhrHZ5miMotHMjvkoTadA4GYPZoOslA28AFW+eshLfFnvw==";

        public List<MPoint> GetCoinData(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}

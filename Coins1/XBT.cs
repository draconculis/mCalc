using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mCoin.Api.Coins;

namespace mCalc.Coins
{
    [Export(typeof(ICoin))]
    public class XBT : ICoin
    {
        public string Name => "Bitcoin";
        public string Code => "XBT";
        public string AltCode => "BCN";

        public string SomeValue { get; set; }

        [ImportingConstructor]
        public XBT([Import("Msg")]string hello)
        {
            SomeValue = hello;
        }
    }
}

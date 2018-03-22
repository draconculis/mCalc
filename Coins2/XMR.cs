using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mCoin.Api.Coins;

namespace Coins2
{
    [Export(typeof(ICoin))]
    public class XMR : ICoin
    {
        public string Name => "Monero";
        public string Code => "XMR";
        public string AltCode => "XMR";
        public string SomeValue { get; set; }

        [ImportingConstructor]
        public XMR([Import("Msg")]string hello)
        {
            SomeValue = hello;
        }

    }
}

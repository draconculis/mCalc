using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mCoin.Api.Coins
{
    public interface ICoin
    {
        string Name { get; }
        string Code { get; }
        string AltCode { get; }

        string SomeValue { get; set; }
    }
}

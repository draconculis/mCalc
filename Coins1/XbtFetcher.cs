using Dek572.Cls;
using mCoin.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mCalc.Coins
{
    public class XbtFetcher : ICoinDataFetcher
    {


        List<MPoint> ICoinDataFetcher.Fetch()
        {
            throw new NotImplementedException();
        }

        List<MPoint> ICoinDataFetcher.Fetch(DateTime from)
        {
            throw new NotImplementedException();
        }

        List<MPoint> ICoinDataFetcher.Fetch(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}

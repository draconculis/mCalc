using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dek572.Cls;

namespace mCoin.Api
{
    public interface ICoinDataFetcher
    {
        List<MPoint> Fetch();

        List<MPoint> Fetch(DateTime from);

        List<MPoint> Fetch(DateTime from, DateTime to);


    }
}

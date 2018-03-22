using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using mCoin.Api.Coins;

namespace mCalc.Models
{
    public class MainModel
    {
        [ImportMany(typeof(ICoin))]
        public List<ICoin> Coins { get; set; }


        public MainModel()
        {

            Compose();
        }

        private void Compose()
        {
            DirectoryCatalog catalog = new DirectoryCatalog("Coins", "*.dll");
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeExportedValue("Msg", "How are You!!!");// Inject stuff into each mefd class
            container.ComposeParts(this);
        }

    }
}

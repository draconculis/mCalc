using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using mCalc.Models;
using mCoin.Api.Coins;

namespace mCalc.ViewModels
{
    public class MainViewModel
    {

        public List<ICoin> Coins => m_MainModel.Coins;

        private readonly MainModel m_MainModel;

        public MainViewModel(MainModel model)
        {
            m_MainModel = model;
        }

        public string SomeText { get; set; } = "Hellooo";
    }
}

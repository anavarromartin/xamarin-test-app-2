using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinDemo2
{
    public partial class MainPage : ContentPage
    {
        public string Fruit { get; set; }
        public MainPage()
        {
            Fruit = Helpers.Secrets.fruit;
            InitializeComponent();

            BindingContext = App.GetViewModel<MainPageViewModel>();
        }
    }
}


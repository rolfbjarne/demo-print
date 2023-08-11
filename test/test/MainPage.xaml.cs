using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using UIKit;
using Foundation;
using CarPlay;
using test.Services;

namespace test
{
    public partial class MainPage : ContentPage
    {
        private UIPrintInteractionController _printInteractionController;

        public MainPage()
        {
            InitializeComponent();


        }
        int count = 0;

        private UIPrintPageRenderer GetUIPrintPageRenderer()
        {
            return new UIPrintPageRenderer();
        }

        void Button_Clicked(object sender, System.EventArgs e)
        {
            onClick();

            return;
        }

        protected virtual async void onClick()
        {
            var printer = DependencyService.Get<IPrinter>();

            if (printer != null)
            {
                printer.SelectPrinter();
            }

        }
    }
}


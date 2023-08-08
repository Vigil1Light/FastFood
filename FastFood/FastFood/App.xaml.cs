using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FastFood.Views;

[assembly: ExportFont("FontAwesome5Regular.otf", Alias = "FontAwesome5Regular")]

namespace FastFood
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

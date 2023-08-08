using FastFood.Models;
using FastFood.Utils;
using FastFood.ViewModels;
using FastFood.Views;
using System;
using Xamarin.Forms;
using System.Threading;

namespace FastFood
{
    public partial class MainPage : ContentPage
    {
        public MainPage(Users user)
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel(user, lstMenus);
        }

        private async void lstMenus_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (App.Current.MainPage.Navigation.NavigationStack.Count == 1)
            {
                IsLoading(true);
                try
                {
                    var selectedProduct = (Menus)e.Item;
                    Thread newThread = new Thread(new ParameterizedThreadStart(LongRunningTask));
                    newThread.Start(selectedProduct.menuType);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message.ToString(), "Ok");
                }      
            }
            lstMenus.SelectedItem = null;
        }

        void OnImageTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event
                DependencyService.Get<Toast>().Show(args.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LongRunningTask(object parameter)
        {
            // Get the parameter value
            string parameterValue = parameter as string;

            // Perform the long-running operation here
            // ...

            // Update UI from the background thread using Device.BeginInvokeOnMainThread
            Device.BeginInvokeOnMainThread(() =>
            {
                // Update UI to indicate that the operation has completed
                Navigation.PushAsync(new IngreditPage(parameterValue), true);
                IsLoading(false);      
            });
        }

        void IsLoading(bool state)
        {
            lstMenus.IsEnabled = !state;
            activity.IsRunning = state;
        }
    }

}

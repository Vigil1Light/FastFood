using FastFood.Models;
using FastFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFood.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngreditPage : ContentPage
	{
		public IngreditPage (string menu)
		{
			InitializeComponent ();
            this.BindingContext = new IngreditViewModel(menu, lstIngredits);
        }

        private async void LstIngredits_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedProduct = (Ingredits)e.Item;

                await DisplayAlert("You selected", selectedProduct.ingreditName, "Select", "Cancel");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
            lstIngredits.SelectedItem = null;
        }
    }
}
using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.StaticClasses;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewSearchResults : ContentPage
    {
        public NewSearchResults()
        {
            InitializeComponent();
            BindingContext = AppData.NewSearchResultPageViewModel;

            TapGestureRecognizer backTap = new TapGestureRecognizer();
            backTap.SetBinding(TapGestureRecognizer.CommandProperty, "BackTapCommand");
            ImgBack.GestureRecognizers.Add(backTap);

            TapGestureRecognizer filterTap = new TapGestureRecognizer();
            filterTap.SetBinding(TapGestureRecognizer.CommandProperty, "FilterTapCommand");
            ImgFilter.GestureRecognizers.Add(filterTap);

            TapGestureRecognizer cartTap = new TapGestureRecognizer();
            cartTap.SetBinding(TapGestureRecognizer.CommandProperty, "CartTapCommand");
            ImgCart.GestureRecognizers.Add(cartTap);
        }

        private async void OnViewMoreTapped(object sender, EventArgs e)
        {
            try
            {
                await AppData.NewSearchResultPageViewModel.OnViewMoreTappedAsync((ProductsResult)((Image)sender).BindingContext);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async void OnAddCartTapped(object sender, EventArgs e)
        {
            try
            {
                await AppData.NewSearchResultPageViewModel.OnAddCartTappedAsync((ProductsResult)((Button)sender).BindingContext);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppData.NewSearchResultPageViewModel.CartListCollectionChanged(null, null);
        }
    }
}
using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.StaticClasses;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartDetailsPage : ContentPage
    {
        public CartDetailsPage()
        {
            InitializeComponent();
            BindingContext = AppData.CartDetailsPageViewModel;

            TapGestureRecognizer backTap = new TapGestureRecognizer();
            backTap.SetBinding(TapGestureRecognizer.CommandProperty, "BackTapCommand");
            ImgBack.GestureRecognizers.Add(backTap);
        }

        private void OnPlusTapped(object sender, EventArgs e)
        {
            try
            {
                AppData.CartDetailsPageViewModel.OnIncreaseTapped((CartModel)((Image)sender).BindingContext);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async void OnMinusTappedAsync(object sender, EventArgs e)
        {
            try
            {
                await AppData.CartDetailsPageViewModel.OnDecreaseTappedAsync((CartModel)((Image)sender).BindingContext);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async void OnDeleteTapped(object sender, EventArgs e)
        {
            try
            {
                await AppData.CartDetailsPageViewModel.OnRemoveTappedAsync((CartModel)((Image)sender).BindingContext);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async void OnViewMoreTapped(object sender, EventArgs e)
        {
            try
            {
                await AppData.FilterPageViewModel.OnViewMoreTappedAsync((ProductsResult)((Image)sender).BindingContext);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }
    }
}
using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        public FilterPage()
        {
            InitializeComponent();
            BindingContext = AppData.FilterPageViewModel;
            TxtDescription.Completed += OnEnterPressed;
            TxtItemCode.Completed += OnEnterPressed;
            SwSparepart.Toggled += OnEnterPressed;

            TapGestureRecognizer backTap = new TapGestureRecognizer();
            backTap.SetBinding(TapGestureRecognizer.CommandProperty, "BackTapCommand");
            ImgBack.GestureRecognizers.Add(backTap);

            TapGestureRecognizer cartTap = new TapGestureRecognizer();
            cartTap.SetBinding(TapGestureRecognizer.CommandProperty, "CartTapCommand");
            ImgCart.GestureRecognizers.Add(cartTap);

            TapGestureRecognizer filterTap = new TapGestureRecognizer();
            filterTap.SetBinding(TapGestureRecognizer.CommandProperty, "FilterTapCommand");
            ImgFilter.GestureRecognizers.Add(filterTap);

            ResultList.ItemAppearing += ListItemAppearingAsync;
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

        public void OnEnterPressed(object sender, EventArgs e)
        {
            try
            {
                AppData.FilterPageViewModel.ApplyFilterTapCommand.Execute(null);
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
                await AppData.FilterPageViewModel.OnAddCartTappedAsync((ProductsResult)((Button)sender).BindingContext);
                await animateAsync();
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async void ListItemAppearingAsync(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if ((e.ItemIndex == AppData.FilterPageViewModel.FilteredResultsList.Count - 1) && ((!AppData.FilterPageViewModel.IsLoading) && (ResultList.IsScrolling && AppData.IsDataToLoad)))
                {
                    ResultList.IsScrolling = false;
                    AppData.FilterPageViewModel.IsLoading = true;
                    AppData.ResultListIndex = AppData.ResultListIndex + 1;
                    await AppData.LoadingListAsync();
                    AppData.FilterPageViewModel.IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }
        private async Task animateAsync()
        {
            ImgCicle.Opacity = 0;           //Opacity Change to 0 means element fully Hide     
            await ImgCicle.FadeTo(1, 400);
            await ImgCicle.ScaleTo(1, 100, Easing.CubicOut);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppData.FilterPageViewModel.CartListCollectionChanged(null, null);
        }
    }
}
using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.Pages;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.ViewModels
{
    public class NewSearchResultsViewModel : BaseViewModel
    {

        private bool isLoading;
        private string cartCount;
        private ObservableCollection<ProductsResult> productsResultList;

        public ICommand BackTapCommand { get; }

        public ICommand FilterTapCommand { get; }

        public ICommand CartTapCommand { get; }

        public ObservableCollection<ProductsResult> ProductsResultsList
        {
            get => productsResultList;
            set
            {
                productsResultList = value;
                OnPropertyChanged(nameof(ProductsResultsList));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public string CartCount
        {
            get => cartCount;
            set
            {
                cartCount = value;
                OnPropertyChanged(nameof(CartCount));

            }
        }

        public NewSearchResultsViewModel()
        {
            BackTapCommand = new Command(async () => { await OnBackTapped(); });
            FilterTapCommand = new Command(async () => { await OnFilterTappedAsync(); });
            CartTapCommand = new Command(async () => { await OnCartTappedAsync(); });
            AppData.CartDetailsPageViewModel.SelectedCartList.CollectionChanged += CartListCollectionChanged;
        }

        private async Task OnBackTapped()
        {
            try
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async Task OnFilterTappedAsync()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await App.Current.MainPage.Navigation.PushModalAsync(new FilterPage());
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }

        private async Task OnCartTappedAsync()
        {
            try
            {
                await AppData.NavigateToCartListAsync();

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        public async Task OnViewMoreTappedAsync(ProductsResult item)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    AppData.DetailsPageViewModel.SelectedProduct = item;
                    await App.Current.MainPage.Navigation.PushModalAsync(new DetailsPage());
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }

        public async Task OnAddCartTappedAsync(ProductsResult item)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await AppData.AddToCartListAsync(item);
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }

        public void CartListCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (AppData.CartDetailsPageViewModel.SelectedCartList != null)
                {
                    CartCount = AppData.CartDetailsPageViewModel.SelectedCartList.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

    }
}

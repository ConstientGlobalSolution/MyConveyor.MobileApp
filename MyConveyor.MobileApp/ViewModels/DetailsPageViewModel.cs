using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.Pages;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.ViewModels
{
    public class DetailsPageViewModel : BaseViewModel
    {
        private bool isLoading;
        private ProductsResult selectedProducts;
        private ImageSource thumbNailSource;
        private string serialNumber, itemCode, itemDescription, amountUsed, cartCount;

        public ICommand BackTapCommand { get; }

        public ICommand AddCartTapCommand { get; }

        public ICommand CartTapCommand { get; }

        public ProductsResult SelectedProduct
        {
            get => selectedProducts;
            set
            {
                try
                {
                    if (value != null)
                    {
                        selectedProducts = value;
                        SerialNumber = value.SerialNumber;
                        ItemCode = value.ItemCode;
                        ItemDescription = value.ItemDescription;
                        AmountUsed = value.AmountUsed.ToString();
                        ThumbnailSource = value.ThumbnailSource;
                        OnPropertyChanged(nameof(SelectedProduct));
                    }

                }
                catch (Exception ex)
                {
                    LogTracking.LogTrace(ex.Message + ex.StackTrace);
                }
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public string SerialNumber
        {
            get
            {
                if (serialNumber == null)
                {
                    serialNumber = "NA";
                }

                return serialNumber;
            }
            set
            {
                if (serialNumber != value)
                {
                    serialNumber = value;
                    OnPropertyChanged(nameof(SerialNumber));
                }

            }
        }

        public string ItemCode
        {
            get
            {
                if (itemCode == null)
                {
                    itemCode = "NA";
                }

                return itemCode;
            }
            set
            {
                if (itemCode != value)
                {
                    itemCode = value;
                    OnPropertyChanged(nameof(ItemCode));
                }

            }
        }

        public string AmountUsed
        {
            get
            {
                if (amountUsed == null)
                {
                    amountUsed = "NA";
                }

                return amountUsed;
            }
            set
            {
                if (amountUsed != value)
                {
                    amountUsed = value;
                    OnPropertyChanged(nameof(AmountUsed));
                }

            }
        }

        public string ItemDescription
        {
            get
            {
                if (itemDescription == null)
                {
                    itemDescription = "NA";
                }

                return itemDescription;
            }
            set
            {
                if (itemDescription != value)
                {
                    itemDescription = value;
                    OnPropertyChanged(nameof(ItemDescription));
                }

            }
        }

        public ImageSource ThumbnailSource

        {
            get => thumbNailSource;
            set
            {
                thumbNailSource = value;
                OnPropertyChanged(nameof(ThumbnailSource));
            }
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

        public DetailsPageViewModel()
        {
            BackTapCommand = new Command(async () => { await OnBackTapped(); });
            AddCartTapCommand = new Command(async () => { await OnAddToCartTappedAsync(); });
            CartTapCommand = new Command(async () => { await OnCartTappedAsync(); });
            AppData.CartDetailsPageViewModel.SelectedCartList.CollectionChanged += CartListCollectionChanged;
        }

        private async Task OnBackTapped()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async Task OnAddToCartTappedAsync()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await AppData.AddToCartListAsync(SelectedProduct);
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
                if (!IsLoading)
                {
                    IsLoading = true;
                    await AppData.NavigateToCartListAsync();
                    IsLoading = false;
                }

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

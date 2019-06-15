using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.DependencyInterfaces;
using MyConveyor.MobileApp.Pages;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private string serialNumber, cartCount;
        private bool isLoading;
        private Color entryBorderColor;

        public ICommand SearchCommand { get; }

        public ICommand QRCodeTapCommand { get; }

        public ICommand CartTapCommand { get; }

        public string SerialNumber
        {
            get => serialNumber;
            set
            {
                serialNumber = value;
                OnPropertyChanged(nameof(SerialNumber));
            }
        }

        public Color EntryBorderColor
        {
            get => entryBorderColor;
            set
            {
                entryBorderColor = value;
                OnPropertyChanged(nameof(EntryBorderColor));
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

        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public SearchPageViewModel()
        {
            try
            {
                EntryBorderColor = Color.Transparent;
                if (AppData.CartDetailsPageViewModel.SelectedCartList != null)
                {
                    CartCount = AppData.CartDetailsPageViewModel.SelectedCartList.Count.ToString();
                }

                SearchCommand = new Command(async () => { await SearchProcessAsync(); });
                QRCodeTapCommand = new Command(async () => { await OnQRCodeTappedAsync(); });
                CartTapCommand = new Command(async () => { await OnCartTappedAsync(); });
                AppData.CartDetailsPageViewModel.SelectedCartList.CollectionChanged += CartListCollectionChanged;
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        private async Task SearchProcessAsync()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    AppData.IsDataToLoad = true;
                    if (string.IsNullOrEmpty(SerialNumber))
                    {
                        EntryBorderColor = Color.Red;
                    }
                    else
                    {
                        AppData.SerialNumber = SerialNumber;
                        AppData.ResultListIndex = 1;
                        AppData.FilterPageViewModel.FilteredResultsList.Clear();
                        await AppData.LoadingListAsync();
                        if (AppData.FilterPageViewModel.FilteredResultsList != null && AppData.FilterPageViewModel.FilteredResultsList.Count > 0)
                        {
                            EntryBorderColor = Color.Transparent;
                            await App.Current.MainPage.Navigation.PushModalAsync(new FilterPage());
                        }
                        else
                        {
                            EntryBorderColor = Color.Red;
                        }
                    }

                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }

        private async Task OnQRCodeTappedAsync()
        {

            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    IQRScanner scanner = DependencyService.Get<IQRScanner>();

                    string result = await scanner.ScanQRAndBarCode();
                    if (result != null)
                    {
                        SerialNumber = result;
                    }

                    IsLoading = false;
                    await SearchProcessAsync();

                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
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

        public void CartListCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    if (AppData.CartDetailsPageViewModel.SelectedCartList != null)
                    {
                        CartCount = AppData.CartDetailsPageViewModel.SelectedCartList.Count.ToString();
                    }

                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

    }
}

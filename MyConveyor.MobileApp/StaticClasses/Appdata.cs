using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.Pages;
using MyConveyor.MobileApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MyConveyor.MobileApp.StaticClasses.ModEnumerations;

namespace MyConveyor.MobileApp.StaticClasses
{
    public static class AppData
    {
        private static NewSearchResultsViewModel newSearchResultPageViewModel;
        private static GetQuotePageViewModel quotePageViewModel;
        private static MessagePageViewModel messagePageViewModel;
        private static FilterPageViewModel filterPageViewModel;
        private static DetailsPageViewModel detailsPageViewModel;
        private static CartDetailsPageViewModel cartDetailsPageViewModel;
        private static string serialNumber;
        private static int resultIndex;

        public static string NetworkWarningMessage { get; set; }

        public static string DirectoryPath { get; set; }

        public static StringBuilder LogDetails { get; set; }

        private static FileSystem fileAccess;

        public static string SerialNumber
        {
            get
            {
                if (serialNumber == null)
                {
                    serialNumber = string.Empty;
                }

                return serialNumber;
            }
            set
            {
                if (serialNumber != value)
                {
                    serialNumber = value;
                }
            }
        }

        public static bool IsDataToLoad { get; set; }

        public static int ResultListIndex
        {
            get => resultIndex;
            set
            {
                if (resultIndex != value)
                {
                    resultIndex = value;
                }
            }
        }

        public static NewSearchResultsViewModel NewSearchResultPageViewModel
        {
            get
            {
                if (newSearchResultPageViewModel == null)
                {
                    newSearchResultPageViewModel = new NewSearchResultsViewModel();
                }

                return newSearchResultPageViewModel;
            }

        }

        public static GetQuotePageViewModel GetQuotePageViewModel
        {
            get
            {
                if (quotePageViewModel == null)
                {
                    quotePageViewModel = new GetQuotePageViewModel();
                }

                return quotePageViewModel;
            }

        }

        public static MessagePageViewModel MessagePageViewModel
        {
            get
            {
                if (messagePageViewModel == null)
                {
                    messagePageViewModel = new MessagePageViewModel();
                }

                return messagePageViewModel;
            }

        }

        public static FilterPageViewModel FilterPageViewModel
        {
            get
            {
                if (filterPageViewModel == null)
                {
                    filterPageViewModel = new FilterPageViewModel();
                }

                return filterPageViewModel;
            }
        }

        public static CartDetailsPageViewModel CartDetailsPageViewModel
        {
            get
            {
                if (cartDetailsPageViewModel == null)
                {
                    cartDetailsPageViewModel = new CartDetailsPageViewModel();
                }

                return cartDetailsPageViewModel;
            }
        }

        public static DetailsPageViewModel DetailsPageViewModel
        {
            get
            {
                if (detailsPageViewModel == null)
                {
                    detailsPageViewModel = new DetailsPageViewModel();
                }

                return detailsPageViewModel;
            }

        }

        public static FileSystem FileAccess
        {
            get
            {
                if (fileAccess == null)
                {
                    fileAccess = new FileSystem();
                }

                return fileAccess;
            }
        }

        public static ImageSource GetimageResource(string base64Image)
        {
            try
            {
                byte[] base64Stream = Convert.FromBase64String(base64Image);
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(base64Stream));
                return imageSource;
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return null;
            }
        }

        public static void LoadCartList()
        {
            string data = AppData.FileAccess.LoadText("CartList.txt");
            if (data != null)
            {
                ObservableCollection<CartModel> cartList = JsonConvert.DeserializeObject<ObservableCollection<CartModel>>(data);
                foreach (CartModel item in cartList)
                {
                    AppData.CartDetailsPageViewModel.SelectedCartList.Add(item);
                }
            }
        }

        public static async Task AddToCartListAsync(ProductsResult item)
        {

            try
            {
                CartModel existingItem = AppData.CartDetailsPageViewModel.SelectedCartList.FirstOrDefault(x => x.Id == item.Id);
                if (existingItem == null)
                {
                    AppData.CartDetailsPageViewModel.SelectedCartList.Add(
                            new CartModel()
                            {
                                Id = item.Id,
                                Image = item.Image,
                                IsSparePart = item.IsSparePart,
                                ItemCode = item.ItemCode,
                                SerialNumber = item.SerialNumber,
                                ItemDescription = item.ItemDescription,
                                AmountUsed = item.AmountUsed,
                                Quantity = 1,
                                ProductId = item.Id
                            });
                    SaveCartDetails();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Already exists", "The selected product is already exists in the cart.", "OK");
                }
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        public static decimal GetDecimalAmount(decimal usedAmount)
        {
            string[] amount = usedAmount.ToString().Split('.');
            if (amount.Length > 1)
            {
                if (Convert.ToInt64(amount[1]) > 0)
                {
                    return Math.Round(usedAmount, 2, MidpointRounding.AwayFromZero);
                }
                return Convert.ToDecimal(amount[0]);
            }
            else
            {
                return usedAmount;
            }
        }
        public static void SaveCartDetails()
        {
            try
            {
                string json = JsonConvert.SerializeObject(AppData.CartDetailsPageViewModel.SelectedCartList);
                AppData.FileAccess.Save("CartList.txt", json);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        public static async Task RemoveFromCartListAsync(CartModel item)
        {
            try
            {
                if (AppData.CartDetailsPageViewModel.SelectedCartList.Contains(item))
                {
                    AppData.CartDetailsPageViewModel.SelectedCartList.Remove(item);
                    SaveCartDetails();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("No items Found", "The selected product is doesn't exists in the cart.", "OK");
                }

                if (AppData.CartDetailsPageViewModel.SelectedCartList.Count == 0)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        public static async Task NavigateToCartListAsync()
        {
            try
            {
                if (AppData.CartDetailsPageViewModel.SelectedCartList?.Count > 0)
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new CartDetailsPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("No items Found", "No products are available in the cart.", "OK");
                }
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }

        public static bool GetConnection()
        {
            bool connectionFlag = true;
            switch (Reachability.InternetConnectionStatus())
            {
                case ReachabilityNetworkStatus.NotReachable:
                    connectionFlag = false;
                    NetworkWarningMessage = "No network connection available.";
                    break;
            }

            if (connectionFlag)
            {
                NetworkWarningMessage = "";
            }

            return connectionFlag;
        }

        public static async Task LoadingListAsync()
        {
            try
            {
                if (AppData.GetConnection())
                {
                    int count = 10;
                    Response<List<ProductsResult>> productsResult = await Constants.ClientApi.GetEntity<Response<List<ProductsResult>>>(Constants.GetFilterDataURI(AppData.SerialNumber, AppData.FilterPageViewModel.ItemCode, AppData.FilterPageViewModel.ItemDescription, AppData.FilterPageViewModel.IsToggled, ResultListIndex, count));

                    if (productsResult != null)
                    {
                        AppData.IsDataToLoad = productsResult.IsSuccess;
                        AppData.SerialNumber = SerialNumber;
                        foreach (ProductsResult item in productsResult.Data)
                        {
                            ProductsResult existingItem = AppData.FilterPageViewModel.FilteredResultsList.FirstOrDefault(x => x.Id == item.Id);
                            if (existingItem == null)
                            {
                                AppData.FilterPageViewModel.FilteredResultsList.Add(item);
                            }
                        }

                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Unable to Fetch data!", AppData.NetworkWarningMessage, "OK");
                }
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
            }
        }
    }
}

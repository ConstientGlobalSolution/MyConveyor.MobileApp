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
    public class CartDetailsPageViewModel : BaseViewModel
    {
        private bool isLoading;
        private string quantity;
        private int quantityCount;
        private ObservableCollection<CartModel> selectedCartList;

        public ICommand BackTapCommand { get; }

        public ICommand GetQuoteTapCommand { get; }

        public ObservableCollection<CartModel> SelectedCartList
        {
            get
            {
                if (selectedCartList == null)
                    selectedCartList = new ObservableCollection<CartModel>();

                return selectedCartList;
            }
            set
            {
                selectedCartList = value;
                OnPropertyChanged(nameof(SelectedCartList));
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public int QuantityCount
        {
            get { return quantityCount; }
            set
            {
                quantityCount = value;
                Quantity = quantityCount.ToString();
                OnPropertyChanged(nameof(QuantityCount));
            }
        }

        public CartDetailsPageViewModel()
        {
            BackTapCommand = new Command(async () => { await OnBackTapped(); });
            GetQuoteTapCommand = new Command(async () => { await OnGetQuoteTapped(); });
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

        private async Task OnGetQuoteTapped()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await App.Current.MainPage.Navigation.PushModalAsync(new GetQuotePage());
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }

        public async Task OnDecreaseTappedAsync(CartModel item)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    if (item.Quantity > 1)
                    {
                        item.Quantity--;
                        AppData.SaveCartDetails();
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Quantity cannot be 0", "You can remove the item using delete.", "OK");

                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }

        public CartModel OnIncreaseTapped(CartModel item)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    item.Quantity = item.Quantity + 1;
                    OnPropertyChanged(nameof(item.Quantity));
                    AppData.SaveCartDetails();
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
            return item;
        }

        public async Task OnRemoveTappedAsync(CartModel item)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await AppData.RemoveFromCartListAsync(item);
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                IsLoading = false;
            }
        }
    }
}

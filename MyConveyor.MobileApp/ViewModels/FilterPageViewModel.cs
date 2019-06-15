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
    public class FilterPageViewModel : BaseViewModel
    {
        private ObservableCollection<ProductsResult> filteredResultList;
        private string itemCode, itemDescription, cartCount;
        private bool isToggled, isLoading, isFilterVisible;
        private Color codeBorderColor, descriptionBorderColor;

        public ICommand BackTapCommand { get; }

        public ICommand ApplyFilterTapCommand { get; }

        public ICommand CartTapCommand { get; }

        public ICommand FilterTapCommand { get; }

        public string ItemCode
        {
            get => itemCode;
            set
            {
                itemCode = value;
                OnPropertyChanged(nameof(ItemCode));
            }
        }

        public Color CodeBorderColor
        {
            get => codeBorderColor;
            set
            {
                codeBorderColor = value;
                OnPropertyChanged(nameof(CodeBorderColor));
            }
        }

        public Color DescriptionBorderColor
        {
            get => descriptionBorderColor;
            set
            {
                descriptionBorderColor = value;
                OnPropertyChanged(nameof(DescriptionBorderColor));
            }
        }

        public string ItemDescription
        {
            get => itemDescription;
            set
            {
                itemDescription = value;
                OnPropertyChanged(nameof(ItemDescription));
            }
        }

        public bool IsToggled
        {
            get => isToggled;
            set
            {
                isToggled = value;
                OnPropertyChanged(nameof(IsToggled));
            }
        }

        public bool IsFilterVisible
        {
            get => isFilterVisible;
            set
            {
                isFilterVisible = value;
                OnPropertyChanged(nameof(IsFilterVisible));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public ObservableCollection<ProductsResult> FilteredResultsList
        {
            get
            {
                if (filteredResultList == null)
                {
                    filteredResultList = new ObservableCollection<ProductsResult>();
                }

                return filteredResultList;
            }
            set
            {
                filteredResultList = value;
                OnPropertyChanged(nameof(FilteredResultsList));
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

        public FilterPageViewModel()
        {
            IsToggled = true;
            IsFilterVisible = false;
            BackTapCommand = new Command(async () => { await OnBackTapped(); });
            ApplyFilterTapCommand = new Command(async () => { await OnApplyFilterTappedAsync(); });
            FilterTapCommand = new Command(() => { OnFilterTapped(); });
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

        private async Task OnApplyFilterTappedAsync()
        {
            try
            {
                if (!IsLoading)
                {
                    AppData.ResultListIndex = 1;
                    IsLoading = true;
                    AppData.IsDataToLoad = true;
                    AppData.FilterPageViewModel.FilteredResultsList?.Clear();
                    IsFilterVisible = false;
                    await AppData.LoadingListAsync();
                    if (AppData.FilterPageViewModel.FilteredResultsList.Count > 0)
                    {
                        CodeBorderColor = Color.Transparent;
                        DescriptionBorderColor = Color.Transparent;
                        IsFilterVisible = false;
                    }
                    else
                    {
                        CodeBorderColor = Color.Red;
                        DescriptionBorderColor = Color.Red;
                        IsFilterVisible = true;
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

        private void OnFilterTapped()
        {
            try
            {
                CodeBorderColor = Color.Transparent;
                DescriptionBorderColor = Color.Transparent;
                IsFilterVisible = !IsFilterVisible;
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

        public async Task OnViewMoreTappedAsync(ProductsResult item)
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    await App.Current.MainPage.Navigation.PushModalAsync(new DetailsPage());
                    AppData.DetailsPageViewModel.SelectedProduct = item;
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
    }
}

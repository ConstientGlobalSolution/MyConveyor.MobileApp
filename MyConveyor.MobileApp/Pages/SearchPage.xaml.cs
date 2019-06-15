using MyConveyor.MobileApp.StaticClasses;
using MyConveyor.MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchPageViewModel();

            TapGestureRecognizer tapQRCode = new TapGestureRecognizer();
            tapQRCode.SetBinding(TapGestureRecognizer.CommandProperty, "QRCodeTapCommand");
            ImgBarCodeScan.GestureRecognizers.Add(tapQRCode);

            TapGestureRecognizer cartTap = new TapGestureRecognizer();
            cartTap.SetBinding(TapGestureRecognizer.CommandProperty, "CartTapCommand");
            ImgCart.GestureRecognizers.Add(cartTap);

            TxtSerialNumber.Focused += (s, e) => { SetLayoutPosition(onFocus: true); };
            TxtSerialNumber.Unfocused += (s, e) => { SetLayoutPosition(onFocus: false); };

        }

        private void SetLayoutPosition(bool onFocus)
        {
            if (onFocus)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    this.SlMainGrid.TranslateTo(0, -100, 50);
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    this.SlMainGrid.TranslateTo(0, 0, 50);
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppData.FilterPageViewModel.ItemCode = null;
            AppData.FilterPageViewModel.ItemDescription = null;
            AppData.FilterPageViewModel.IsToggled = true;
            AppData.FilterPageViewModel.IsFilterVisible = false;
        }
    }


}

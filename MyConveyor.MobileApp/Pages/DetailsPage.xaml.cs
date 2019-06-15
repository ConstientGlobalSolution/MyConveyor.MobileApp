using MyConveyor.MobileApp.StaticClasses;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
            BindingContext = AppData.DetailsPageViewModel;

            TapGestureRecognizer backTap = new TapGestureRecognizer();
            backTap.SetBinding(TapGestureRecognizer.CommandProperty, "BackTapCommand");
            ImgBack.GestureRecognizers.Add(backTap);

            TapGestureRecognizer cartTap = new TapGestureRecognizer();
            cartTap.SetBinding(TapGestureRecognizer.CommandProperty, "CartTapCommand");
            ImgCart.GestureRecognizers.Add(cartTap);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppData.DetailsPageViewModel.CartListCollectionChanged(null, null);
        }
    }
}
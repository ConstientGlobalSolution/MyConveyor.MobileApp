using MyConveyor.MobileApp.StaticClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetQuotePage : ContentPage
    {
        public GetQuotePage()
        {
            InitializeComponent();
            BindingContext = AppData.GetQuotePageViewModel;

            TapGestureRecognizer backTap = new TapGestureRecognizer();
            backTap.SetBinding(TapGestureRecognizer.CommandProperty, "BackTapCommand");
            ImgBack.GestureRecognizers.Add(backTap);
        }
    }
}
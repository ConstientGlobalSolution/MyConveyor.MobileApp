using MyConveyor.MobileApp.StaticClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyConveyor.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        public MessagePage()
        {
            InitializeComponent();
            BindingContext = AppData.MessagePageViewModel;

            TapGestureRecognizer backTap = new TapGestureRecognizer();
            backTap.SetBinding(TapGestureRecognizer.CommandProperty, "BackTapCommand");
            ImgBack.GestureRecognizers.Add(backTap);
        }

    }
}
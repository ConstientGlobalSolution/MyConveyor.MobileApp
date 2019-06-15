using MyConveyor.MobileApp.Classes;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.ViewModels
{
    public class MessagePageViewModel : BaseViewModel
    {
        private Color textColor;
        private string text;
        private bool isLoading;

        public ICommand BackTapCommand { get; }

        public ICommand OkCommand { get; }

        public string MessageText
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged(nameof(MessageText));

            }
        }

        public Color MessageTextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                OnPropertyChanged(nameof(MessageTextColor));

            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public MessagePageViewModel()
        {
            OkCommand = new Command(async () => { await OnBackTapped(); });
            BackTapCommand = new Command(async () => { await OnBackTapped(); });
        }

        private async Task OnBackTapped()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    if (MessageTextColor != Color.Red)
                        App.Current.MainPage = new MyConveyor.MobileApp.Pages.SearchPage();
                    else
                        await App.Current.MainPage.Navigation.PopModalAsync();
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

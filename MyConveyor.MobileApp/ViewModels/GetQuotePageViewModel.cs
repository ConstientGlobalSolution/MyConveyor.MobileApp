using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.Models;
using MyConveyor.MobileApp.Pages;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.ViewModels
{

    public class GetQuotePageViewModel : BaseViewModel
    {
        private string name, companyName, phoneNumber, emailAddress;
        private bool isLoading;
        private User userDetails;
        private Color nameBorderColor, companyBorderColor, phoneBorderColor, mailBorderColor;

        public ICommand SubmitCommand { get; }

        public ICommand BackTapCommand { get; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public User UserDetails
        {
            get
            {
                if (userDetails == null)
                {
                    userDetails = new User();
                    userDetails.PropertyChanged += UserDetails_PropertyChanged;
                }

                return userDetails;
            }
            set
            {
                userDetails = value;
                OnPropertyChanged(nameof(UserDetails));
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string EmailAddress
        {
            get => emailAddress;
            set
            {
                string input = value;
                emailAddress = input.Replace(" ", "");
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        public Color NameBorderColor
        {
            get => nameBorderColor;
            set
            {
                nameBorderColor = value;
                OnPropertyChanged(nameof(NameBorderColor));
            }
        }

        public Color CompanyBorderColor
        {
            get => companyBorderColor;
            set
            {
                companyBorderColor = value;
                OnPropertyChanged(nameof(CompanyBorderColor));
            }
        }

        public Color PhoneBorderColor
        {
            get => phoneBorderColor;
            set
            {
                phoneBorderColor = value;
                OnPropertyChanged(nameof(PhoneBorderColor));
            }
        }

        public Color MailBorderColor
        {
            get => mailBorderColor;
            set
            {
                mailBorderColor = value;
                OnPropertyChanged(nameof(MailBorderColor));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set { isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public GetQuotePageViewModel()
        {
            NameBorderColor = CompanyBorderColor = PhoneBorderColor = MailBorderColor = Color.Transparent;
            SubmitCommand = new Command(async () => { await OnSubmitTapped(); });
            BackTapCommand = new Command(async () => { await OnBackTapped(); });
        }

        private async Task OnSubmitTapped()
        {
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    if (ValidateFields())
                    {
                        List<OrderItemModel> cartItems = new List<OrderItemModel>();
                        foreach (CartModel item in AppData.CartDetailsPageViewModel.SelectedCartList)
                        {
                            if (item.Quantity > 0)
                            {
                                cartItems.Add(new OrderItemModel { ProductId = item.Id, Quantity = item.Quantity });
                            }
                        }

                        OrderModel postData = new OrderModel()
                        {
                            User = UserDetails,
                            Items = cartItems
                        };

                        if (AppData.GetConnection())
                        {
                            Response<OrderModel> result = await Constants.ClientApi.PostEntity<OrderModel, Response<OrderModel>>(Constants.PostOrderListURI(), postData, new Response<OrderModel>());

                            if (result != null && result.IsSuccess)
                            {
                                AppData.CartDetailsPageViewModel.SelectedCartList.Clear();
                                AppData.MessagePageViewModel.MessageText = "Quotation requested Successfully.";
                                AppData.MessagePageViewModel.MessageTextColor = Color.Green;
                                AppData.SaveCartDetails();
                                await App.Current.MainPage.Navigation.PushModalAsync(new MessagePage());
                                IsLoading = false;
                                return;
                            }

                        }

                        IsLoading = false;
                        AppData.MessagePageViewModel.MessageText = "Unable to place an order! " + AppData.NetworkWarningMessage;
                        AppData.MessagePageViewModel.MessageTextColor = Color.Red;
                        await App.Current.MainPage.Navigation.PushModalAsync(new MessagePage());
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

        private void UserDetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FullName")
            {
                NameBorderColor = UserDetails.FullName.RedIfNullOrWhitespace(this);
            }

            if (e.PropertyName == "CompanyName")
            {
                CompanyBorderColor = UserDetails.CompanyName.RedIfNullOrWhitespace(this);
            }

            if (e.PropertyName == "Phone")
            {
                PhoneBorderColor = UserDetails.Phone.RedIfNullOrWhitespace(this);
            }

            if (e.PropertyName == "Email")
            {
                MailBorderColor = UserDetails.Email.RedIfNullOrWhitespace(this);
            }
        }

        private bool ValidateFields()
        {
            HasTriedSave = true;
            NameBorderColor = UserDetails.FullName.RedIfNullOrWhitespace(this);
            CompanyBorderColor = UserDetails.CompanyName.RedIfNullOrWhitespace(this);
            PhoneBorderColor = UserDetails.Phone.RedIfNullOrWhitespace(this);
            MailBorderColor = UserDetails.Email.RedIfNullOrWhitespace(this);

            bool validEmail = UserDetails.Email.ValidRegexIfNotNull(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            MailBorderColor = (!validEmail).RedIfInvalid();

            return validEmail && !ViewModelExtensions.IsAnyEmpty(this, UserDetails.FullName, UserDetails.CompanyName, UserDetails.Email, UserDetails.Phone);
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

    }
}


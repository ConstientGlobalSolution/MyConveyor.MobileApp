using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyConveyor.MobileApp.Models
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string email;
        private string fullName;
        private string phone;
        private string companyName;

        public string FullName
        {
            get => fullName?.TrimEnd();
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        public string CompanyName
        {
            get => companyName?.TrimEnd();
            set { companyName = value; OnPropertyChanged(nameof(CompanyName)); }
        }

        public string Email
        {
            get => email?.TrimEnd();
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Phone
        {
            get => phone?.TrimEnd();
            set
            {
                phone = value; OnPropertyChanged(nameof(Phone));
            }
        }

    }
}

using MyConveyor.MobileApp.StaticClasses;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyConveyor.MobileApp.Models
{
    public class CartModel : ProductsResult, INotifyPropertyChanged
    {
        private decimal quantity;

        public long ProductId { get; set; }
       
        public decimal Quantity
        {
            get => quantity;
            set
            {
                quantity = AppData.GetDecimalAmount(value);               
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

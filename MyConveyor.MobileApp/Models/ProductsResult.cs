using MyConveyor.MobileApp.StaticClasses;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.Models
{
    public class ProductsResult
    {
        private string base64string;
        private decimal amountUsed;

        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public int OrderNumber { get; set; }

        public int SuborderNumber { get; set; }

        public int JoborderNumber { get; set; }

        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }

        public decimal AmountUsed
        {
            get => amountUsed;
            set => amountUsed = AppData.GetDecimalAmount(value);
        }

        public bool IsSparePart { get; set; }

        public string Image
        {
            get => base64string;
            set
            {
                base64string = value;
                ThumbnailSource = AppData.GetimageResource(base64string);
            }
        }

        [JsonIgnore]
        public ImageSource ThumbnailSource { get; set; }

        [JsonIgnore]
        public Color RowColor { get; set; }

        [JsonIgnore]
        public bool IsNotSparePart => !IsSparePart;
    }
}

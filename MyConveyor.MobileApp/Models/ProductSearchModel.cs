namespace MyConveyor.MobileApp.Models
{
    public class ProductSearchModel
    {
        public string SerialNumber { get; set; }

        public bool IsSparePart { get; set; } = true;

        public int PageNumber { get; set; } // PageNumber must be greater than 0

        public int PageSize { get; set; }    // PageSize must be greater than 0
    }
}

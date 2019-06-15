namespace MyConveyor.MobileApp.Models
{
    public class ProductFilterModel
    {
        public string SerialNumber { get; set; }   //SerialNumber is required

        public bool IsSparePart { get; set; }

        public int PageNumber { get; set; } // PageNumber must be greater than 0

        public int PageSize { get; set; }    // PageSize must be greater than 0

        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }

    }
}

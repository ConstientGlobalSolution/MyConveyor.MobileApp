namespace MyConveyor.MobileApp.Models
{
    public class OrderItemModel
    {
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal AmountUsed { get; set; }
    }
}

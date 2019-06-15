using System.Collections.Generic;

namespace MyConveyor.MobileApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public bool IsEmailSent { get; set; }

        public User User { get; set; }

        public List<OrderItemModel> Items { get; set; }

    }
}

using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoyTechAPITest.Models
{
    public class User
    {
        public int Id { get; set; }
        [NotMapped]
        public int OrdersQuantity { get => GetOrdersQuantity(); set { value = OrdersQuantity; } }
        [NotMapped]
        public decimal AllOrdersSum { get => GetAllOrdersSum(); set { value = AllOrdersSum; } }
        public List<Order> Orders { get; set; } = new List<Order>();
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string RefreshToken { get; set; } = string.Empty;
        [NotMapped]
        public DateTime TokenCreated { get; set; }
        [NotMapped]
        public DateTime TokenExpires { get; set; }
        public User()
        {

        }
        private decimal GetAllOrdersSum()
        {
            var all_purchases = this.Orders.Select(x => x.InOrderProducts).ToList();
            List<decimal> orders_sums = new List<decimal>();
            foreach (var item in all_purchases)
            {
                orders_sums.Add(item.Sum(x => x.Product.Price * x.Quantity));
            }
            return orders_sums.Sum();
        }
        private int GetOrdersQuantity()
        {
            return this.Orders.Count;
        }

    }
}

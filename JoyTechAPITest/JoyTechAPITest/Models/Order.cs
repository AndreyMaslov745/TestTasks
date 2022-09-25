using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace JoyTechAPITest.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [NotMapped]
        public decimal OrderSum{ get => GetOrderSum(); set { value = OrderSum; } }
        [NotMapped]
        public int ProductsQuantity { get=> GetProductsQuantity() ; set { value = ProductsQuantity; } }
        public List<ProductInOrder> InOrderProducts { get; set; } = new List<ProductInOrder>();
        public Order()
        {
            
        }
        private decimal GetOrderSum()
        {
            return InOrderProducts.Sum(x => x.Product.Price * x.Quantity);
        }
        private int GetProductsQuantity()
        {
            return (int)InOrderProducts.Sum(x => x.Quantity);
        }
    }
}

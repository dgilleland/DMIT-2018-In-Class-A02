using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.Entities.DTOs
{
    public class Order
    {
        public int BillID { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
    public class OrderItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

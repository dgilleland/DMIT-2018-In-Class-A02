using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string Description { get; set; }
        // Use decimal for the SQL money data type
        public decimal CurrentPrice { get; set; }
        // Use decimal for the SQL money data type
        public decimal CurrentCost { get; set; }
        public bool Active { get; set; }
        public int? Calories { get; set; }
        public string Comment { get; set; }
        public int MenuCategoryID { get; set; }
        // Navigation Properties
        public virtual MenuCategory MenuCategory { get; set; }
    }
}

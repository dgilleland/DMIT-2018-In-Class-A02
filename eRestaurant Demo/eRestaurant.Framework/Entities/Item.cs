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
        public double CurrentPrice { get; set; }
        public double CurrentCost { get; set; }
        public bool Active { get; set; }
        public int? Calories { get; set; }
        public string Comment { get; set; }
        public int MenuCategoryID { get; set; }
        // Navigation Properties
        public virtual MenuCategory MenuCategory { get; set; }
    }
}

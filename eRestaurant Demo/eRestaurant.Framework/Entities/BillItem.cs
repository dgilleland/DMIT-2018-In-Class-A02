using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // [Column]
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.Entities
{
    public class BillItem
    {
        // The database table that this will map to has a 
        // composite primary key.
        [Key, Column(Order = 1)]
        public int BillID { get; set; }
        [Key, Column(Order = 2)]
        public int ItemID { get; set; }

        // Quantity must be a min of 1 and a max of 20
        [Required(), Range(1, 20)]
        public int Quantity { get; set; }
        // Sale price is between $0 and $50, inclusive
        [Required, Range(0.00, 50.00)]
        public decimal SalePrice { get; set; }
        // Unit cost is between $0.01 and $30 inclusive
        [Required, Range(0.01, 30.00)]
        public decimal UnitCost { get; set; }
        public string Notes { get; set; }

        // Navigation Properties
        public virtual Bill Bill { get; set; }
        public virtual Item Item { get; set; }
    }
}

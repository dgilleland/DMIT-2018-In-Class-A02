using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.Entities
{
    public class Bill
    {
        public int BillID { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? OrderPlaced { get; set; }
        public int NumberInParty { get; set; }
        public bool PaidStatus { get; set; }
        public int WaiterID { get; set; }
        public int? TableID { get; set; }
        public int? ReservationID { get; set; }
        public bool OrderReady { get; set; }
        public string Comment { get; set; }

        // Navigation Properties
        public virtual ICollection<BillItem> Items { get; set; }
        public virtual Waiter Waiter { get; set; }
        public virtual Table Table { get; set; }

        public Bill()
        {
            BillDate = DateTime.Now;
        }
    }
}

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
        public TimeSpan? OrderPlaced { get; set; }
        public TimeSpan? OrderPaid { get; set; } // oops, missed this one...
        public TimeSpan? OrderServed { get; set; } // missed another.... :/
        public int NumberInParty { get; set; }
        public bool PaidStatus { get; set; }
        public int WaiterID { get; set; }
        public int? TableID { get; set; }
        public int? ReservationID { get; set; }
        public TimeSpan? OrderReady { get; set; }
        public string Comment { get; set; }

        // Navigation Properties
        public virtual ICollection<BillItem> Items { get; set; }
        public virtual Waiter Waiter { get; set; }
        public virtual Table Table { get; set; }
        public virtual Reservation Reservation { get; set; }

        public Bill()
        {
            BillDate = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.Entities.DTOs
{
    public class ReservationCollection
    {
        public int Hour { get; set; }
        public virtual ICollection<ReservationSummary> Reservations { get; set; }
    }
}

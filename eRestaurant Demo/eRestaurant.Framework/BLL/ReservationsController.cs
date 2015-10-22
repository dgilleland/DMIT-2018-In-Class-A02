using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // comes with Entity Framework
using eRestaurant.Framework.DAL;
using eRestaurant.Framework.Entities;
using eRestaurant.Framework.Entities.POCOs;
using eRestaurant.Framework.Entities.DTOs;

namespace eRestaurant.Framework.BLL
{
    [DataObject]
    public class ReservationsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ActiveEvent> ListActiveEvents()
        {
            using (var context = new RestaurantContext())
            {
                var result = from eachRow in context.SpecialEvents
                             where eachRow.Active
                             select new ActiveEvent()
                             {
                                 Code = eachRow.EventCode,
                                 Description = eachRow.Description
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<DailyReservation> ListUpcomingReservations(string eventCode)
        {
            throw new NotImplementedException();
        }
    }
}

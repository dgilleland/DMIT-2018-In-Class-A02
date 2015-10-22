using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // comes with Entity Framework
using eRestaurant.Framework.DAL;
using eRestaurant.Framework.Entities;
using eRestaurant.Framework.Entities.POCOs;

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eRestaurant.Framework.Entities.DTOs;
using eRestaurant.Framework.DAL;
using eRestaurant.Framework.Entities;

namespace eRestaurant.Framework.BLL
{
    [DataObject]
    public class SeatingController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReservationCollection> ReservationsForDate(DateTime date)
        {
            using (var context = new RestaurantContext())
            {
                var result = from eachRow in context.Reservations
                             where eachRow.ReservationDate.Year == date.Year
                                && eachRow.ReservationDate.Month == date.Month
                                && eachRow.ReservationDate.Day == date.Day
                                && eachRow.ReservationStatus == Reservation.Booked
                             select new ReservationSummary() //DTOs
                             {
                                 Name = eachRow.CustomerName,
                                 Date = eachRow.ReservationDate,
                                 NumberInParty = eachRow.NumberInParty,
                                 Status = eachRow.ReservationStatus.ToString(),
                                 Event = eachRow.SpecialEvent.Description,
                                 Contact = eachRow.ContactPhone
                             };
                var finalResult = from eachItem in result
                                  group eachItem by eachItem.Date.Hour into itemGroup
                                  select new ReservationCollection()
                                  {
                                      Hour = itemGroup.Key,
                                      Reservations = itemGroup.ToList()
                                  };
                return finalResult.ToList();
            }
        }
    }
}

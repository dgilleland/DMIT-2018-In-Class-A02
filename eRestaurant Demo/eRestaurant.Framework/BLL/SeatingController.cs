using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eRestaurant.Framework.Entities.DTOs;
using eRestaurant.Framework.DAL;
using eRestaurant.Framework.Entities;
using System.Data.Entity;

namespace eRestaurant.Framework.BLL
{
    [DataObject]
    public class SeatingController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SeatingSummary> SeatingByDateTime(DateTime date, TimeSpan time)
        {
            using (var context = new RestaurantContext())
            {
                // copy/paste my code from LinqPad (adapt)
                #region Adapted from LinqPad
                // Find out information on the tables in the restaurant at a specific date/time
                // Create a date and time object to use for sample input data
                //var date = Bills.Max(b => b.BillDate).Date; // just want the date portion
                //var time = Bills.Max(b => b.BillDate).TimeOfDay.Add(new TimeSpan(0, 30, 0));
                //date.Add(time).Dump("The test date/time I am using");

                // Step 1 - Get the table info along with any walk-in bills and reservation bills
                //          for a specific time slot
                // since the data type of context.Tables is a DbSet<Table>,
                // that means I can add a bit of extra to my LINQ query to
                // declare that the variable data is of type Table
var step1 = from Table data in context.Tables
            select new
            {
                Table = data.TableNumber,
                Seating = data.Capacity,
                // This sub-query gets the bills for walk-in customers
                WalkIns = from Bill walkIn in data.Bills
                            where walkIn.BillDate.Year == date.Year
                                && walkIn.BillDate.Month == date.Month
                                && walkIn.BillDate.Day == date.Day
                                // The following won't work in EF to Entities
                                //&& (!walkIn.OrderPaid.HasValue || walkIn.OrderPaid.Value >= time)
                                //
                                && DbFunctions.CreateTime(walkIn.BillDate.Hour, walkIn.BillDate.Minute, walkIn.BillDate.Second) <= time
                            select walkIn,
                // This sub-query gets the bills for reservations
                Reservations = from Reservation booking in data.Reservations
                                from reservationParty in booking.Bills
                                where reservationParty.BillDate.Year == date.Year
                                    && reservationParty.BillDate.Month == date.Month
                                    && reservationParty.BillDate.Day == date.Day
                                    && DbFunctions.CreateTime(reservationParty.BillDate.Hour, reservationParty.BillDate.Minute, reservationParty.BillDate.Second) <= time
                                    //&& (!reservationParty.OrderPaid.HasValue || reservationParty.OrderPaid >= time)
                                select reservationParty
            };
                //step1.Dump("Results of Step 1");			

                // Step 2 - Union the walk-in bills and the reservation bills while extracting the
                //          relevant bill info.
                //          .ToList() helps resolve the error
                //          "Types in Union or Concat are constructed incompatibly"
                var step2 = from data in step1.ToList()
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                CommonBilling = from Bill info in data.WalkIns.Union(data.Reservations)
                                                select new // trimmed down billing info
                                                {
                                                    BillID = info.BillID,
                                                    BillTotal = info.Items.Sum(bi => bi.Quantity * bi.SalePrice),
                                                    Waiter = info.Waiter.FirstName,
                                                    Reservation = info.Reservation
                                                }
                            };
                //step2.Dump("Results of Step 2");			

                // Step 3 - Get just the first CommonBilling item
                //          (presumes no overlaps can occur - i.e., two groups at the same
                //          table at the same time)
                var step3 = from data in step2.ToList()
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.CommonBilling.Count() > 0,
                                // .FirstOrDefault() is effectively "flattening" my collection of 1 item into a
                                // single object whose properties I can get in Step 4 using the dot (.) operator
                                CommonBilling = data.CommonBilling.FirstOrDefault()
                            };
                //step3.Dump();

                // Step4 - Build our intended seating summary info
                var step4 = from data in step3
                            select new SeatingSummary() // the DTO class to use in my BLL
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.Taken,
                                // use a ternary expression to conditionally get the bill id (if it exists)
                                BillID = data.Taken ?               // if(data.Taken)
                                         data.CommonBilling.BillID  // value to use if true
                                       : (int?)null,               // value to use if false
                                BillTotal = data.Taken ?
                                            data.CommonBilling.BillTotal : (decimal?)null,
                                Waiter = data.Taken ? data.CommonBilling.Waiter : (string)null,
                                ReservationName = data.Taken ?
                                                  (data.CommonBilling.Reservation != null ?
                                                   data.CommonBilling.Reservation.CustomerName :
                                                   (string)null)
                                                : (string)null
                            };
                //step4.Dump("The final, boiled-down list of table occupancy at a point in time");
                #endregion
                return step4.ToList();
            }
        }

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

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> AvailableSeatingByDateTime(DateTime date, TimeSpan time)
        {
            var result = from seats in SeatingByDateTime(date, time)
                         where !seats.Taken
                         select seats;
            return result.ToList();
        }
        public void SeatCustomer(DateTime when, int reservationId, List<byte> tables, int waiterId)
        {
            var availableSeats = AvailableSeatingByDateTime(when.Date, when.TimeOfDay);
            using (var context = new RestaurantContext())
            {
                List<string> errors = new List<string>();
                // Rule checking:
                // - Reservation must be in Booked status
                // - Table must be available - typically a direct check on the table, but proxied based on the mocked time here
                // - Table must be big enough for the # of customers
                var reservation = context.Reservations.Find(reservationId);
                if (reservation == null)
                    errors.Add("The specified reservation does not exist");
                else if (reservation.ReservationStatus != Reservation.Booked)
                    errors.Add("The reservation's status is not valid for seating. Only booked reservations can be seated.");
                var capacity = 0;
                foreach (var tableNumber in tables)
                {
                    if (!availableSeats.Exists(x => x.Table == tableNumber))
                        errors.Add("Table " + tableNumber + " is currently not available");
                    else
                        capacity += availableSeats.Single(x => x.Table == tableNumber).Seating;
                }
                if (capacity < reservation.NumberInParty)
                    errors.Add("Insufficient seating capacity for number of customers. Alternate tables must be used.");
                if (errors.Count > 0)
                    throw new Exception("Unable to seat customer");
                // 1) Create a blank bill with assigned waiter
                Bill seatedCustomer = new Bill()
                {
                    BillDate = when,
                    NumberInParty = reservation.NumberInParty,
                    WaiterID = waiterId,
                    ReservationID = reservation.ReservationID
                };
                context.Bills.Add(seatedCustomer);
                // 2) Add the tables for the reservation and change the reservation's status to arrived
                foreach (var tableNumber in tables)
                    reservation.Tables.Add(context.Tables.Single(x => x.TableNumber == tableNumber));
                reservation.ReservationStatus = Reservation.Arrived;
                var updatable = context.Entry(context.Reservations.Attach(reservation));
                updatable.Property(x => x.ReservationStatus).IsModified = true;
                //updatable.Reference(x=>x.Tables).
                // 3) Save changes
                context.SaveChanges();
            }
            //string message = String.Format("Not yet implemented. Need to seat reservation {0} for waiter {1} at tables {2}", reservationId, waiterId, string.Join(", ", tables));
            //throw new NotImplementedException(message);
        }
    }
}

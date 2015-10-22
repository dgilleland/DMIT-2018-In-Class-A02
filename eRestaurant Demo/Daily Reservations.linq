<Query Kind="Statements">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var step1 = from eachRow in Reservations
where eachRow.ReservationStatus == 'B' // use "B" in Visual Studio
		// TBA - && eachRow has the correct EventCode...
orderby eachRow.ReservationDate
//select eachRow
group eachRow by new { eachRow.ReservationDate.Month, eachRow.ReservationDate.Day };
// into dailyReservation
var result = from dailyReservation in step1.ToList()
select new // DailyReservation() // Create a DTO class called DailyReservation
{
	Month = dailyReservation.Key.Month,
	Day = dailyReservation.Key.Day,
	Reservations = 	from booking in dailyReservation
				   	select new // Booking() // Create a Booking POCO class
					{
						Name = booking.CustomerName,
						Time = booking.ReservationDate.TimeOfDay,
						NumberInParty = booking.NumberInParty,
						Phone = booking.ContactPhone,
						Event = booking.SpecialEvents == null
						      ? (string)null
							  : booking.SpecialEvents.Description
					}
};
result.Dump();









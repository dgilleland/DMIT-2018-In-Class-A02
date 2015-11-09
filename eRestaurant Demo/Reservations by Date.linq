<Query Kind="Program">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	// Model how we will be calling/using our BLL methods
	var date = new DateTime(2014, 10, 24);
	var result = ReservationsForDate(date);
	result.Dump(); // output the result in LinqPad
}

// Define other methods and classes here
// Model the BLL methods that we need to make
public object ReservationsForDate(DateTime date)
{
	var result = from eachRow in Reservations
	             where eachRow.ReservationDate.Year == date.Year
				    && eachRow.ReservationDate.Month == date.Month
					&& eachRow.ReservationDate.Day == date.Day
					&& eachRow.ReservationStatus == 'B' // Reservation.Booked
				 select new ReservationSummary() //DTOs
				{
					Name = eachRow.CustomerName,
					Date = eachRow.ReservationDate,
					NumberInParty = eachRow.NumberInParty,
					Status = eachRow.ReservationStatus.ToString(),
					Event = eachRow.SpecialEvents.Description,
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

// Data Transfer Object
public class ReservationCollection
{
	public int Hour { get; set; }
	public virtual ICollection<ReservationSummary> Reservations { get; set; }
}

public class ReservationSummary
{
	public string Name { get; set; }
	public DateTime Date { get; set; }
	public int NumberInParty { get; set;}
	public string Status { get; set; }
	public string Event { get; set; }
	public string Contact { get; set; }
}













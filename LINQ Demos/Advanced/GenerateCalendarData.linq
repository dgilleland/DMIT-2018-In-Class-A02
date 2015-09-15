<Query Kind="Program">
  <Connection>
    <ID>db681f81-d76e-40d8-ba6e-38f797e7b291</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// ADVANCED:
//  Generate a list of all the reservations for each day in the month, as a Calendar view
void Main()
{
    GenerateCalendarTable(2014, 9).Dump();
    var cal = GenerateCalendarData(2014, 9);
    cal.Dump();
}

// Define other methods and classes here

// GenerateCalendarTable from
// http://henrylu.me/2014/07/30/creating-an-html-table-in-one-linq-statement-2/
public string GenerateCalendarTable(int year, int month)
{
    DateTime firstDayOfMonth = new DateTime(year, month, 1);
    int offset = (int)firstDayOfMonth.DayOfWeek;
    int maxValue = DateTime.DaysInMonth(year, month);
    int columnCount = 7;
    int rowCount = (int)Math.Ceiling((offset + maxValue) / (decimal)columnCount);
 
    return
        "<table>" +
            Enumerable.Range(1, rowCount).Select(r =>
            "<tr>" +
                Enumerable.Range(columnCount * (r - 1) + 1 - offset, columnCount)
                .Select(n => "<td>" + (n >= 1 && n <= maxValue ? n.ToString() + "<br />" + new DateTime(year, month, n).ToShortDateString() : "&nbsp;") + "</td>")
                .Aggregate((html, cell) => html + cell) +
            "</tr>"
            ).Aggregate((html, row) => html + row) +
        "</table>";
}

public IEnumerable<dynamic> GenerateCalendarData(int year, int month)
{
    DateTime firstDayOfMonth = new DateTime(year, month, 1);
    int offset = (int)firstDayOfMonth.DayOfWeek;
    int maxValue = DateTime.DaysInMonth(year, month);
    int columnCount = 7;
    int rowCount = (int)Math.Ceiling((offset + maxValue) / (decimal)columnCount);
 
    return  Enumerable.Range(1, rowCount)
                .Select(r =>
                    Enumerable.Range(columnCount * (r - 1) + 1 - offset, columnCount)
                        .Select(n =>new { 
                            date = (n >= 1 && n <= maxValue ? new DateTime(year, month, n) : (DateTime?)null),
                            reservations = from booking in Reservations
                                           where booking.ReservationDate.Year == year
                                              && booking.ReservationDate.Month == month
                                              && booking.ReservationDate.Day == n
                                           select booking
                                                 
                        }));
}
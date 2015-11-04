<Query Kind="Expression">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//from row in Bills
//orderby row.BillDate descending
//select row.BillDate
Bills.Max(row => row.BillDate)
// Among the bills, get the max (largest) row such that I look at/use the row's BillDate

<Query Kind="Statements">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

/* Example 1: Querying data from eRestaurant */
var result = from row in Tables
			 where row.Capacity > 3
			 select row;
		
// The following line won't work in your VS project....			 
result.Dump(); // the .Dump() method is an extension method in LinqPad - it's not in .NET
/* Example 2: Query a simple array of strings */
string[] names = {"Dan", "Don", "Sam", "Dwayne", "Laurel", "Steve"};
names.Dump();
var shortList = from person in names
                where person.StartsWith("D")
				select person;
shortList.Dump();
/* Example 3: Find the most recent Bill and its total */
// Get all the bills that have been placed
var allBills = from receipt in Bills
               where receipt.OrderPlaced.HasValue
               select new // declaring an "anonymous type" on-the-fly
               {          // using an initializer list to set the properties
                  BillDate = receipt.BillDate,  
				  IsReady = receipt.OrderReady
			   };
allBills.Count().Dump(); // show the count of items
allBills.Take(5).Dump(); // show the first 5 bills








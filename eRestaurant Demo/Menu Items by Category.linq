<Query Kind="Expression">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from cat in MenuCategories
orderby cat.Description
select new
{
	Description = cat.Description,
	MenuItems = from item in cat.Items
			    where item.Active
				orderby item.Description
				select new
				{
					Description = item.Description,
					Price = item.CurrentPrice,
					Calories = item.Calories,
					Comment = item.Comment
				}
}
<Query Kind="Expression">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from food in Items
group food by food.MenuCategory into foodGroup
select new
{
	CategoryID = foodGroup.Key.MenuCategoryID,
	Category = foodGroup.Key.Description,
	Count = foodGroup.Count(),
	MenuItems = foodGroup.ToList()
}
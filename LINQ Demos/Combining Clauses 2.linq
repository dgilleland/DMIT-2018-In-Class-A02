<Query Kind="Expression">
  <Connection>
    <ID>dcae7eaf-13af-4ac8-b6c9-a6b72c1e20d5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// List all the menu items that are Entrees or Beverages
// in order from most to least expensive
from food in Items

where food.MenuCategory.Description == "Entree"
   || food.MenuCategory.Description == "Beverage"
   
orderby food.CurrentPrice descending

group food by food.MenuCategoryID into result

select result
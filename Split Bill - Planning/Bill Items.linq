<Query Kind="Expression">
  <Connection>
    <ID>db681f81-d76e-40d8-ba6e-38f797e7b291</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// Select the bill items for a specific bill (in this case, the unpaid bill)
from data in Bills
where data.BillID == 10975
select new //Order()
{
    BillID = data.BillID,
    Items = (from info in data.BillItems
            select new //OrderItem()
            {
                ItemName = info.Item.Description,
                Price = info.SalePrice,
                Quantity = info.Quantity
            }).ToList()
}
<Query Kind="Expression">
  <Connection>
    <ID>db681f81-d76e-40d8-ba6e-38f797e7b291</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

// What bills are not yet paid?
from data in Bills
where !data.PaidStatus
   && data.BillItems.Count() > 0
select new //ListDataItem()
{
    DisplayText ="Bill " +  data.BillID.ToString(),// + " (Table " + data.Table.TableNumber.ToString() + ")",
    KeyValue = data.BillID
}

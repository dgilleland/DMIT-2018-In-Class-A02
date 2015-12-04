using eRestaurant.Framework.DAL;
using eRestaurant.Framework.Entities;
using eRestaurant.Framework.Entities.DTOs;
using eRestaurant.Framework.Entities.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.BLL
{
    [DataObject]
    public class WaiterController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<UnpaidBill> ListUnpaidBills()
        {
            using(var context = new RestaurantContext())
            {
                var result = from data in context.Bills
                             where !data.PaidStatus
                                && data.Items.Count() > 0
                             select new UnpaidBill()
                             {
                                 DisplayText = "Bill " + data.BillID.ToString(),
                                 BillId = data.BillID
                             };
                return result.ToList();
            }
        }

        public Order GetBill(int billId)
        {
            using (var context = new RestaurantContext())
            {
                var result = from data in context.Bills
                             where data.BillID == billId
                             select new Order()
                             {
                                 BillID = data.BillID,
                                 // IEnumerable<OrderItem>
                                 Items = (from info in data.Items
                                          select new OrderItem()
                                          {
                                              ItemName = info.Item.Description,
                                              Price = info.SalePrice,
                                              Quantity = info.Quantity
                                          }).ToList()
                             };
                return result.FirstOrDefault();
            }
        }

        public void SplitBill(int billId, List<OrderItem> updatesToOriginalBillItems, List<OrderItem> newBillItems)
        {
            // Split the bill in two.....
            using (var context = new RestaurantContext())
            {
                // TODO: 0) Validation :)
                // 1) Get the bill
                var bill = context.Bills.Find(billId);
                if (bill == null) throw new ArgumentException("Invalid Bill ID - does not exist");

                // 2) Loop through bill items, if item no in original, remove
                List<BillItem> toRemove = new List<BillItem>();
                // This loop identifies/references each item to remove
                foreach(var item in bill.Items) // the items already in the DB
                {
                    bool inOriginal = updatesToOriginalBillItems.Any(
                                         x => x.ItemName == item.Item.Description);
                    bool inNewItems = newBillItems.Any(
                                         x => x.ItemName == item.Item.Description);
                    if(!inOriginal)
                    {
                        if (!inNewItems)
                            throw new Exception("Hey - someone's got to pay for that!");
                        toRemove.Add(item);
                    }
                }
                // Actually remove the item from the database.....
                foreach (var item in toRemove)
                    context.BillItems.Remove(item);
                
                // 3) Make a new bill
                var newBill = new Bill()
                {
                    BillDate = bill.BillDate, // some info from original bill
                    Comment = "Split from bill# " + bill.BillID,
                    NumberInParty = bill.NumberInParty, // meh
                    OrderPlaced = bill.OrderPlaced,
                    OrderReady = bill.OrderReady,
                    OrderServed = bill.OrderServed,
                    WaiterID = bill.WaiterID
                    // TODO: thorny question about rules around splitting a bill for
                    //       a single table vs. reservation
                };

                // 4) Add the new moved items to the new bill
                if (newBill.Items == null)
                    newBill.Items = new HashSet<BillItem>();
                foreach(var item in toRemove)
                {
                    newBill.Items.Add(new BillItem()
                        {
                            ItemID = item.ItemID,
                            Notes = item.Notes,
                            Quantity = item.Quantity,
                            SalePrice = item.SalePrice,
                            UnitCost = item.UnitCost
                        });
                }

                // 5) Add the new bill to the context
                context.Bills.Add(newBill);
                // 6) Save the changes...
                context.SaveChanges(); // Call this only ONCE at the end - TRANSACTION
            }
        }
    }
}

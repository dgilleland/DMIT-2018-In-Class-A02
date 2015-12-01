using eRestaurant.DAL;
using eRestaurant.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
    [DataObject]
    public class WaiterController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ListDataItem> ListActiveBills()
        {
            using (var context = new RestaurantContext())
            {
                var result = from data in context.Bills
                             where !data.PaidStatus
                                && data.Items.Count() > 0
                             select new ListDataItem()
                             {
                                 DisplayText = data.BillID.ToString() + " (Table " + data.Table.TableNumber.ToString() + ")",
                                 KeyValue = data.BillID
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

        public void SplitBill(int billId, List<OrderItem> originalItems, List<OrderItem> newItems)
        {
            // TODO: Actually go through and split that bill into two
            throw new NotImplementedException();
        }
    }
}

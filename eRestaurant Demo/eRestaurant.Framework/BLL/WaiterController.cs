using eRestaurant.Framework.DAL;
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
    }
}

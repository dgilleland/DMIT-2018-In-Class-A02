using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using eRestaurant.Framework.Entities.POCOs; // for CategoryMenuItem
using eRestaurant.Framework.DAL;

namespace eRestaurant.Framework.BLL
{
    [DataObject]
    public class ReportController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryMenuItem> GetReportCategoryMenuItems()
        {
            using(var context = new RestaurantContext())
            {
                var results = from cat in context.Items
                              orderby cat.MenuCategory.Description, cat.Description
                              select new CategoryMenuItem()
                              {
                                  CategoryDescription = cat.MenuCategory.Description,
                                  ItemDescription = cat.Description,
                                  Price = cat.CurrentPrice,
                                  Calories = cat.Calories,
                                  Comment = cat.Comment
                              };
                return results.ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // for [DataObject] et.al attributes
using eRestaurant.Framework.DAL; // for RestaurantContext
using eRestaurant.Framework.Entities; // for DB Entity Classes
using System.Data.Entity; // for the Lambda version of the Include() method

namespace eRestaurant.Framework.BLL
{
    [DataObject]
    public class MenuController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Item> ListMenuItems()
        {
            using (var context = new RestaurantContext())
            {
                return context.Items.ToList();
            }
        }
    }
}

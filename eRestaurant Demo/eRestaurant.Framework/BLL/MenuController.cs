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
                // Note: To use the Lambda or Method style of Include(),
                // you need to have the following namespace reference:
                // use System.Data.Entity;
                // The .Include() method on the DbSet<T> class performs
                // "eager loading" of the data.
                return context.Items.Include(it => it.MenuCategory).ToList();
            }
        }
    }
}

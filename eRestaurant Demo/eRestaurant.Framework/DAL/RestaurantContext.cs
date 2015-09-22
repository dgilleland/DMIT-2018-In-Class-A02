using eRestaurant.Framework.Entities; // Needed for Entity classes
using System;
using System.Collections.Generic;
using System.Data.Entity; // Needed for the DbContext and other EF classes
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.DAL
{
    // By making our DAL class internal, that means outside projects can't directly
    // access our Data Access Layer (they will have to go through our BLL to do stuff)
    internal class RestaurantContext : DbContext
    {
        public RestaurantContext()
            : base("DefaultConnection")
        { }

        // One property for each Table/Entity in the database
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}

using eRestaurant.Framework.DAL;
using eRestaurant.Framework.DAL.Security;
using eRestaurant.Framework.Entities.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.BLL.Security
{
    public class UserManager : UserManager<ApplicationUser>
    {
        #region Constants
        // Ideally, we should probably put some/all of these in the Web.config file
        // underneath the <appSettings> element.
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        private const string STR_EMAIL_FORMAT = "{0}@eRestaurant.tba";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        #region Constructor(s)
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }
        #endregion

        #region Custom Methods
        public void AddDefaultUsers()
        {
            using (var context = new RestaurantContext())
            {
                // 1) Create user accounts for all the waiters
                var employees = from data in context.Waiters
                                where !data.ReleaseDate.HasValue // they are employed
                                select data;
                foreach(var person in employees)
                {
                    // Check if they exist as a user
                    // if (NOT any of the Users exist such that =>
                    //  u represents a given user where
                    //      the user's WaiterID has a value
                    //      AND the user's WaiterID matches this employee's WaiterID)
                    if (!Users.Any(u => u.WaiterID.HasValue && u.WaiterID.Value == person.WaiterID))
                    {
                        // Add them as a user....
                        string userName = string.Format(STR_USERNAME_FORMAT, person.FirstName, person.LastName);
                        var appUser = new ApplicationUser()
                        {
                            UserName = userName,
                            Email = string.Format(STR_EMAIL_FORMAT, userName),
                            WaiterID = person.WaiterID
                        };
                        // NOTE: The following line of code needs the use the this keyword
                        // in order to have access to the Create() extension method.
                        this.Create(appUser, STR_DEFAULT_PASSWORD);
                    }
                }
            }

            // 2) Create a web-master user
            if(!Users.Any(u=> u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                // Add the web-master
                var webMasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };
                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);
            }
        }
        #endregion
    }
}

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


    }
}

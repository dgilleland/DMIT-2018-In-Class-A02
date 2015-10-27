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
    public class RoleManager : RoleManager<IdentityRole>
    {
        #region Constructor(s)
        public RoleManager()
            : base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {
        }
        #endregion

        #region Custom methods
        public void AddDefaultRoles()
        {
            foreach(string roleName in SecurityRoles.DefaultSecurityRoles)
            {
                // Check if it exists
                if (!Roles.Any(r => r.Name.Equals(roleName)))
                    this.Create(new IdentityRole(roleName));
            }
        }

        #endregion
    }
}

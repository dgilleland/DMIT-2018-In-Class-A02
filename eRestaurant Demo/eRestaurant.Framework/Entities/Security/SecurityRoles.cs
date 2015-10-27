using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Framework.Entities.Security
{
    internal static class SecurityRoles
    {
        public const string WebsiteAdmins = "WebsiteAdmins";
        public const string RegisteredUsers = "RegisteredUsers";
        public const string Staff = "Staff";
        public static List<string> DefaultSecurityRoles
        {
            get
            {
                List<string> values = new List<string>();
                values.Add(WebsiteAdmins);
                values.Add(RegisteredUsers);
                values.Add(Staff);
                return values;
            }
        }
    }
}

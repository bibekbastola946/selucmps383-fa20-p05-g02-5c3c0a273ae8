using System.Collections.Generic;
using FA20.P05.Web.Features.StaffMembers;
using Microsoft.AspNetCore.Identity;

namespace FA20.P05.Web.Features.Authentication
{
    public class User : IdentityUser<int>
    {
        public virtual ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public int? StaffId { get; set; }
        public virtual Staff Staff { get; set; }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FA20.P05.Web.Features.Authentication
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
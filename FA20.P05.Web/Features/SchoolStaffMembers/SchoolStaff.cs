using FA20.P05.Web.Features.Schools;
using FA20.P05.Web.Features.StaffMembers;

namespace FA20.P05.Web.Features.SchoolStaffMembers
{
    public class SchoolStaff
    {
        public int Id { get; set; }

        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }

        public int SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}
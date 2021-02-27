using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA20.P05.Web.Features.SchoolStaffMembers
{
    public class SchoolStaffConfiguration : IEntityTypeConfiguration<SchoolStaff>
    {
        public void Configure(EntityTypeBuilder<SchoolStaff> builder)
        {
        }
    }
}
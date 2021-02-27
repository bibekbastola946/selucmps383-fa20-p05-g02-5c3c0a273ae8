using System;
using System.Collections.Generic;
using FA20.P05.Web.Features.Authentication;
using FA20.P05.Web.Features.SchoolStaffMembers;
using FA20.P05.Web.Features.TemperatureRecords;

namespace FA20.P05.Web.Features.StaffMembers
{
    public class Staff
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public virtual ICollection<SchoolStaff> Schools { get; set; } = new List<SchoolStaff>();

        public virtual ICollection<TemperatureRecord> TemperatureRecords { get; set; } = new List<TemperatureRecord>();

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
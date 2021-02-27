using System.Collections.Generic;
using FA20.P05.Web.Features.SchoolStaffMembers;
using FA20.P05.Web.Features.Shared;
using FA20.P05.Web.Features.TemperatureRecords;

namespace FA20.P05.Web.Features.Schools
{
    public class School
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int SchoolPopulation { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<SchoolStaff> Staff { get; set; } = new List<SchoolStaff>();

        public virtual ICollection<TemperatureRecord> TemperatureRecords { get; set; } = new List<TemperatureRecord>();
    }
}
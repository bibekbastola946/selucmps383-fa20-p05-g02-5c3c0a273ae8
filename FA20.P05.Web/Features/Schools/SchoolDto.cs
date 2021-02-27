using FA20.P05.Web.Features.Shared;

namespace FA20.P05.Web.Features.Schools
{
    public class SchoolDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int SchoolPopulation { get; set; }

        public AddressDto Address { get; set; }
    }
}
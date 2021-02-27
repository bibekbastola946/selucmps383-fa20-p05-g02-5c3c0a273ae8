using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FA20.P05.Web.Features.Shared
{
    [Owned]
    public class Address
    {
        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(5)]
        public string Zip { get; set; }
    }
}
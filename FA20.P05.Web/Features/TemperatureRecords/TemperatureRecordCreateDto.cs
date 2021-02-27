using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FA20.P05.Web.Features.TemperatureRecords
{
    public class TemperatureRecordCreateDto
    {
        public int SchoolId { get; set; }

        public double temperatureFahrenheit { get; set; }
         
        public string Qrcode { get; set; }
    }
}

namespace FA20.P05.Web.Features.TemperatureRecords
{
    public class TemperatureRecordDto
    {
        public int Id { get; set; }

        public int SchoolId { get; set; }

        public double temperatureFahrenheit { get; set; }
       
         public string Qrcode { get; set; }
    }
}
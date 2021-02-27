using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA20.P05.Web.Features.TemperatureRecords
{
    public class TemperatureRecordConfiguration : IEntityTypeConfiguration<TemperatureRecord>
    {
        public void Configure(EntityTypeBuilder<TemperatureRecord> builder)
        {
        }
    }
}
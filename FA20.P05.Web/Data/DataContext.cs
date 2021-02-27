using FA20.P05.Web.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FA20.P05.Web.Features.TemperatureRecords;

namespace FA20.P05.Web.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this looks for all "IEntityTypeConfiguration<T>" records in the same assembly as the DataContext
            // in our case, that is the web project - all .cs files there are under that assembly (oversimplification)
            // this way, we can have several files responsible for the entities, rather than a really big DataContext class
            // this is important to help maintain single responsibility
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            base.OnModelCreating(modelBuilder);

            var userRoleBuilder = modelBuilder.Entity<UserRole>();

            userRoleBuilder.HasKey(x => new { x.UserId, x.RoleId });

            userRoleBuilder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            userRoleBuilder.HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<FA20.P05.Web.Features.TemperatureRecords.TemperatureRecordDto> TemperatureRecordDto { get; set; }
    }
}
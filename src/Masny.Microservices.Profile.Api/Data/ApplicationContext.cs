using Microsoft.EntityFrameworkCore;

namespace Masny.Microservices.Profile.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        /// <inheritdoc/>
        public DbSet<Models.Profile> Profiles { get; set; }
    }
}

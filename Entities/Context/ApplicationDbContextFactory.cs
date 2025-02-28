using Microsoft.EntityFrameworkCore;

namespace API.Entities.Context
{
    public class ApplicationDbContextFactory(DbContextOptions<ApplicationDbContext> options) 
        : IApplicationDbContextFactory
    {
        private readonly DbContextOptions<ApplicationDbContext> _options = options;

        public ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_options);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace API.Entities.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskDetail> TaskDetail { get; set; }
    }
}

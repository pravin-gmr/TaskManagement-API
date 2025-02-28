namespace API.Entities.Context
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext CreateDbContext();
    }
}

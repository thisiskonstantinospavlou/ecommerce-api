namespace Data.EF.Infrastructure
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

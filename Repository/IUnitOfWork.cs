using API.Entities;

namespace API.Repository
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveAsync();
        IBaseRepo<TaskDetail> TaskDetail { get; }
    }
}

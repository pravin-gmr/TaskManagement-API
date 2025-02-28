using API.Models;
using API.Models.Pagination;

namespace API.Services
{
    public interface ITaskService
    {
        Task<bool> AddTaskAsync(TaskDetailAddModel model);
        Task<bool> UpdateTaskAsync(TaskDetailAddModel model);
        Task<bool> UpdateTaskStatusAsync(int id, string status);
        Task<bool> DeleteTaskAsync(int id);
        Task<TaskDetailViewModel> GetTaskByIdAsync(int id);
        Task<PaginationData<TaskDetailViewModel>> GetTasksByPageAsync(int pageNo, int pageSize);
    }
}

using API.Common.Helper;
using API.Common.Utilities;
using API.Entities;
using API.Models;
using API.Models.Pagination;
using API.Repository;
using System.Linq.Expressions;
using static API.Common.Utilities.Enums;

namespace API.Services.Impl
{
    public class TaskService(ILogger<TaskService> logger, IUnitOfWork unitOfWork) : ITaskService
    {
        private readonly ILogger<TaskService> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// Adds a new task to the database.
        /// </summary>
        public async Task<bool> AddTaskAsync(TaskDetailAddModel model)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            try
            {
                var task = new TaskDetail
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    Status = Enums.ApprovalStatus.Pending.GetDescription(),
                    IsActive = true,
                    SavedOn = DateTime.Now
                };

                await _unitOfWork.TaskDetail.AddAsync(task);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonFunctions.GetExceptionLogMessage(ex));
                return false;
            }
        }

        /// <summary>
        /// Updates an existing task's details.
        /// </summary>
        public async Task<bool> UpdateTaskAsync(TaskDetailAddModel model)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            try
            {
                if (!Enum.TryParse(model.Status, true, out ApprovalStatus parsedStatus))
                    parsedStatus = ApprovalStatus.Pending;

                var task = await _unitOfWork.TaskDetail.GetAsync(x => x.TaskId == model.Id && x.IsActive);
                if (task == null)
                {
                    _logger.LogWarning("Task with ID {id} not found.", model.Id);
                    return false;
                }

                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate;
                task.Status = parsedStatus.GetDescription();
                task.ModifiedOn = DateTime.Now;

                await _unitOfWork.TaskDetail.UpdateAsync(task);
                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonFunctions.GetExceptionLogMessage(ex));
                return false;
            }
        }

        /// <summary>
        /// Updates the status of an existing task.
        /// </summary>
        public async Task<bool> UpdateTaskStatusAsync(int id, string status)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            try
            {
                if (!Enum.TryParse(status, true, out ApprovalStatus parsedStatus))
                    parsedStatus = ApprovalStatus.Pending;

                var task = await _unitOfWork.TaskDetail.GetAsync(x => x.TaskId == id && x.IsActive);
                if (task == null)
                {
                    _logger.LogWarning("Task with ID {id} not found.", id);
                    return false;
                }

                task.Status = parsedStatus.GetDescription();
                task.ModifiedOn = DateTime.Now;

                await _unitOfWork.TaskDetail.UpdateAsync(task);

                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonFunctions.GetExceptionLogMessage(ex));
                return false;
            }
        }

        /// <summary>
        /// Deletes a task by setting it as inactive.
        /// </summary>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            try
            {
                var taskDetail = await _unitOfWork.TaskDetail.GetAsync(x => x.TaskId == id && x.IsActive);
                if (taskDetail == null)
                {
                    _logger.LogWarning("Task with ID {id} not found.", id);
                    return false;
                }

                taskDetail.IsActive = false;
                taskDetail.ModifiedOn = DateTime.Now;

                await _unitOfWork.TaskDetail.UpdateAsync(taskDetail);

                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonFunctions.GetExceptionLogMessage(ex));
                return false;
            }
        }

        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        public async Task<TaskDetailViewModel> GetTaskByIdAsync(int id)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            try
            {
                var taskDetail = await _unitOfWork.TaskDetail.GetAsync(x => x.TaskId == id && x.IsActive);
                if (taskDetail == null) return null;

                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return new TaskDetailViewModel
                {
                    Id = taskDetail.TaskId,
                    Title = taskDetail.Title,
                    Description = taskDetail.Description,
                    DueDate = taskDetail.DueDate,
                    Status = taskDetail.Status
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonFunctions.GetExceptionLogMessage(ex));
                return null;
            }
        }

        /// <summary>
        /// Retrieves tasks with pagination.
        /// </summary>
        public async Task<PaginationData<TaskDetailViewModel>> GetTasksByPageAsync(int pageNo, int pageSize)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            try
            {
                var taskList = new PaginationData<TaskDetailViewModel>(pageNo, pageSize);
                Expression<Func<TaskDetail, bool>> filter = x => x.IsActive;
                Expression<Func<TaskDetail, dynamic>> orderBy = x => x.SavedOn;

                var paginationData = await _unitOfWork.TaskDetail.GetAllByPaginationAsync(filter, orderBy, pageNo, pageSize);
                if (paginationData?.Data is not null)
                {
                    taskList.Data = paginationData.Data.Select(x => new TaskDetailViewModel
                    {
                        Id = x.TaskId,
                        Title = x.Title,
                        Description = x.Description,
                        DueDate = x.DueDate,
                        Status = x.Status
                    }).ToList();
                }
                taskList.TotalCount = paginationData!.TotalCount;

                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return taskList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CommonFunctions.GetExceptionLogMessage(ex));
                return null;
            }
        }
    }
}

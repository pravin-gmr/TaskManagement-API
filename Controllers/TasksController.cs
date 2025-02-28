using API.Common.Helper;
using API.Common.Utilities;
using API.Models;
using API.Models.Common;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using static API.Common.Utilities.Enums;

namespace API.Controllers
{
    [Route("task/[action]")]
    public class TasksController(ILogger<TasksController> logger, ITaskService taskService) : BaseApiController
    {
        private readonly ILogger<TasksController> _logger = logger;
        private readonly ITaskService _taskService = taskService;

        /// <summary>
        /// Adds a new task.
        /// </summary>
        [HttpPost]
        [ActionName("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> AddTask(TaskDetailAddModel model)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());

            if (!Enum.TryParse(model.Status, true, out ApprovalStatus parsedStatus))
            {
                return BadRequest("Invalid status value. Allowed values: Pending, Progress, Completed.");
            }

            bool response = await _taskService.AddTaskAsync(model);

            if (response)
            {
                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return SuccessMessage(Messages.saveSuccessfulMessage);
            }
            return ErrorMessage(Messages.saveFailedMessage);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        [HttpPut]
        [ActionName("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> UpdateTask(TaskDetailAddModel model)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());

            if (!Enum.TryParse(model.Status, true, out ApprovalStatus parsedStatus))
            {
                return BadRequest("Invalid status value. Allowed values: Pending, Progress, Completed.");
            }

            bool response = await _taskService.UpdateTaskAsync(model);

            if (response)
            {
                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return SuccessMessage(Messages.updateSuccessfulMessage);
            }
            return ErrorMessage(Messages.updateFailedfulMessage);
        }

        /// <summary>
        /// Updates the status of a specific task.
        /// </summary>
        [HttpPatch("{id:int}/{status}")]
        [ActionName("update-status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> UpdateTaskStatus(int id, string status)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());

            if (!Enum.TryParse(status, true, out ApprovalStatus parsedStatus))
            {
                return BadRequest("Invalid status value. Allowed values: Pending, Progress, Completed.");
            }

            bool response = await _taskService.UpdateTaskStatusAsync(id, status);

            if (response)
            {
                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return SuccessMessage(Messages.updateSuccessfulMessage);
            }
            return ErrorMessage(Messages.updateFailedfulMessage);
        }

        /// <summary>
        /// Deletes a specific task by ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ActionName("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<string>))]
        public async Task<IActionResult> DeleteTaskStatus(int id)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());

            bool response = await _taskService.DeleteTaskAsync(id);

            if (response)
            {
                _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
                return SuccessMessage(Messages.deleteSuccessfulMessage);
            }
            return ErrorMessage(Messages.deleteFailedMessage);
        }

        /// <summary>
        /// Retrieves task details by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ActionName("get-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDetailViewModel))]
        public async Task<IActionResult> GetTaskById(int id)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            var data = await _taskService.GetTaskByIdAsync(id);
            if (data == null) return WarningMessage(Messages.notFoundMessage);
            _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
            var apiResponse = ApiResponseModel<TaskDetailViewModel>.GetDataResponse(data);
            return Ok(apiResponse);
        }

        /// <summary>
        /// Retrieves a paginated list of tasks.
        /// </summary>
        [HttpGet("{pageNo:int}/{pageSize:int}")]
        [ActionName("get-by-page")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel<List<TaskDetailViewModel>>))]
        public async Task<IActionResult> GetTasksByPage(int pageNo, int pageSize)
        {
            _logger.LogInformation(CommonFunctions.GetInitiatedLogMessage());
            var paginationData = await _taskService.GetTasksByPageAsync(pageNo, pageSize);
            if (paginationData == null) return WarningMessage(Messages.notFoundMessage);
            _logger.LogInformation(CommonFunctions.GetCompletedLogMessage());
            var apiResponse = ApiResponseModel<List<TaskDetailViewModel>>.GetDataResponse(paginationData.Data, (int)paginationData.TotalCount, pageSize, pageNo);
            return Ok(apiResponse);
        }
    }
}

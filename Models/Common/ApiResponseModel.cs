using API.Common.Helper;
using System.Text.Json.Serialization;
using static API.Common.Utilities.Enums;

namespace API.Models.Common
{
    public class ApiResponseModel<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("messages")]
        public List<string>? Messages { get; set; } = new List<string>();

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("pageNo")]
        public int PageNo { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("status")]
        public string? Status;

        public static ApiResponseModel<T> GetSuccessResponse(string message)
        {
            return new ApiResponseModel<T>
            {
                IsSuccessful = true,
                Message = message,
                Status = ResponseStatus.Success.GetDescription()
            };
        }
        
        public static ApiResponseModel<T> GetInfoResponse(string message)
        {
            return new ApiResponseModel<T>
            {
                IsSuccessful = true,
                Message = message,
                Status = ResponseStatus.Info.GetDescription()
            };
        }

        public static ApiResponseModel<T> GetErrorResponse(string message)
        {
            return new ApiResponseModel<T>
            {
                IsSuccessful = false,
                Message = message,
                Status = ResponseStatus.Failed.GetDescription()
            };
        }

        public static ApiResponseModel<T> GetErrorResponse(List<string> Errors)
        {
            return new ApiResponseModel<T>
            {
                IsSuccessful = false,
                Messages = Errors,
                Status = ResponseStatus.Failed.GetDescription()
            };
        }

        public static ApiResponseModel<T> GetDeleteResponse(bool result)
        {
            return new ApiResponseModel<T>
            {
                IsSuccessful = result,
                Message = result ? "Deleted Successfully" : "Failed to Delete!!!",
                Status = result ? ResponseStatus.Success.GetDescription() : ResponseStatus.Failed.GetDescription()

            };
        }

        public static ApiResponseModel<T> GetDataResponse(T? data, int totalRecord = 0, int pageSize = 0, int pageNo = 1)
        {
            var property = typeof(T).GetProperty("Count");
            var count = totalRecord == 0 ? (property == null ? 1 : Convert.ToInt32(property.GetValue(data))) : totalRecord;

            return new ApiResponseModel<T>
            {
                IsSuccessful = true,
                Data = data,
                TotalCount = count,
                PageSize = pageSize,
                PageNo = pageNo,
                Status = ResponseStatus.Success.GetDescription()
            };
        }
    }
}

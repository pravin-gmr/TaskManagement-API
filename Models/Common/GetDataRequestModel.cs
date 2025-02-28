using System.Text.Json.Serialization;

namespace API.Models.Common
{
    public class GetDataRequestModel
    {
        [JsonPropertyName("fromDate")]
        public DateTime FromDate { get; set; }

        [JsonPropertyName("toDate")]
        public DateTime ToDate { get; set; }

        [JsonPropertyName("pageNo")]
        public int PageNo { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }
}

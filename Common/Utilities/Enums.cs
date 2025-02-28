using System.ComponentModel;

namespace API.Common.Utilities
{
    public static class Enums
    {
        public enum ResponseStatus
        {
            [Description("Success")]
            Success,
            [Description("Info")]
            Info,
            [Description("Failed")]
            Failed
        }

        public enum ApprovalStatus
        {
            [Description("Pending")]
            Pending,
            [Description("InProgress")]
            InProgress,
            [Description("Completed")]
            Completed
        }
    }
}

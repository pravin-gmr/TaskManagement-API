using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace API.Common.Helper
{
    public static class CommonFunctions
    {
        public static string GetRandomString(int length)
        {
            Random randomNumber = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[randomNumber.Next(s.Length)]).ToArray());
        }

        public static string GetRandomNumber()
        {
            return new Random().Next(1000, 9999).ToString("0000");
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                                   "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

        public static int CalculateAge(DateTime? dob)
        {
            if (dob == null) return 0;

            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dob.Value.Year;

            if (currentDate.Month < dob.Value.Month || (currentDate.Month == dob.Value.Month && currentDate.Day < dob.Value.Day))
            {
                age--;
            }

            return age;
        }

        public static string CheckIntNullAndGetEmptyString(int? value)
        {
            if (value.HasValue && value > 0)
            {
                return value.ToString() ?? string.Empty;
            }

            return string.Empty;
        }

        public static string? GetInitiatedLogMessage([CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            string message = $"Initiated {Path.GetFileNameWithoutExtension(filePath)} - {memberName}";
            return message;
        }

        public static string? GetCompletedLogMessage([CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            string message = $"Completed {Path.GetFileNameWithoutExtension(filePath)} - {memberName}";
            return message;
        }

        public static string? GetExceptionLogMessage(Exception ex, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "")
        {
            string message = $"{Path.GetFileNameWithoutExtension(filePath)} - {memberName} : Exception occurred while processing! - {ex}";
            return message;
        }

        public static int GetDaysInMonth(string monthName, int year)
        {
            int monthNumber = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
            return DateTime.DaysInMonth(year, monthNumber);
        }

        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

        public static async Task DelayedMethod(int delayMilliseconds)
        {
            await Task.Delay(delayMilliseconds);
        }

    }
}

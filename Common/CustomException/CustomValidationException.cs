using FluentValidation.Results;

namespace API.Common.CustomException
{
    [Serializable]
    public class CustomValidationException : Exception
    {
        public List<string>? Errors { get; }

        public CustomValidationException() : base("Validation failed.")
        {
            Errors = new List<string>();
        }

        public CustomValidationException(string message) : base(message)
        {
            Errors = new List<string>();
            Errors!.Add(message);
        }

        public CustomValidationException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        public CustomValidationException(string message, List<ValidationFailure>? errors) : base(message)
        {
            Errors = new List<string>();
            foreach (var error in errors!)
            {
                if (!Errors!.Any(x => x == error.ErrorMessage))
                {
                    Errors!.Add(error.ErrorMessage);
                }
            }
        }
    }
}

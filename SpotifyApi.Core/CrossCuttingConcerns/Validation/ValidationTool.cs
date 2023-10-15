using FluentValidation;

namespace SpotifyApi.Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                string errorMessages = "Validation Errors: ";
                foreach (var error in result.Errors)
                {
                    errorMessages += error.ErrorMessage;
                }
                ErrorResultValidation errorResult = new()
                {
                    Data = null,
                    Message = errorMessages,
                    MessageCode = result.Errors.ToString()
                };
                throw new ValidationException(errorMessages);
            }
        }

        public class ErrorResultValidation
        {
            public string Data { get; set; }
            public string Message { get; set; }
            public string MessageCode { get; set; }

        }
    }
}

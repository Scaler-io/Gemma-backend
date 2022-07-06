using Gemma.Shared.Constants;

namespace Gemma.Shared.Common
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ApiResponse(string code, string message = "")
        {
            Code = code;
            Message = !string.IsNullOrEmpty(message) ? message : GetDefaultMessage(message);
        }

        protected virtual string GetDefaultMessage(string statusCode)
        {
            return statusCode switch
            {
                ErrorCodes.NotFound         =>   ErrorMessages.NotFound,
                ErrorCodes.Operationfailed  =>   ErrorMessages.Operationfailed,
                ErrorCodes.Unauthorized     =>   ErrorMessages.Unauthorized,
                ErrorCodes.BadRequest       =>   ErrorMessages.BadRequest,
                _                           =>   string.Empty
            };
        }
    }
}

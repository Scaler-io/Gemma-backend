using Gemma.Shared.Constants;

namespace Gemma.Shared.Common
{
    public class ApiExceptionResponse: ApiResponse
    {
        public string StackTrace { get; set; }

        public ApiExceptionResponse(string errorMessage="", string stackTrace="")
            :base(ErrorCodes.InternalServerError, errorMessage)
        {
            StackTrace = stackTrace;    
        }
    }
}

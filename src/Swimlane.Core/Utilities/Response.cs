namespace Swimlane.Core.Utilities
{
    public enum ErrorCode
    {
        None,
        Validation,
        NotFound,
        Unauthorized,
        InvalidCredentials,
        InvalidToken,
        TokenExpired,
        Unknown
    }

    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public List<AppError> Errors { get; set; } = new();

        public static Response<T> SuccessResponse(T data, string message = "Success") =>
            new() { Success = true, Message = message, Data = data, ErrorCode = ErrorCode.None };

        public static Response<T> ErrorResponse(ErrorCode code, string message, List<AppError>? errors = null)
        {
            var errorList = errors ?? new List<AppError>();
            errorList.Add(new AppError(code, message));
            return new Response<T>
            {
                Success = false,
                Message = message,
                ErrorCode = code,
                Errors = errorList
            };
        }
    }

    public class AppError
    {
        #region # Init

        public AppError() { }

        public AppError(ErrorCode code, string error)
        {
            Code = code;
            Error = error;
        }

        #endregion

        public ErrorCode Code { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}

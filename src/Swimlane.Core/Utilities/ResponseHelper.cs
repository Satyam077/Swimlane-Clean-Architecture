namespace Swimlane.Core.Utilities
{
    public static class ResponseHelper
    {
        public static Response<T> TryCatch<T>(Func<Response<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return Response<T>.ErrorResponse(
                    ErrorCode.Unknown,
                    "Unexpected error",
                    new List<AppError>
                    {
                    new AppError(ErrorCode.Unknown, ex.Message)
                    }
                );
            }
        }

        public static async Task<Response<T>> TryCatchAsync<T>(Func<Task<Response<T>>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return Response<T>.ErrorResponse(
                    ErrorCode.Unknown,
                    "Unexpected error",
                    new List<AppError>
                    {
                    new AppError(ErrorCode.Unknown, ex.Message)
                    }
                );
            }
        }
    }
}

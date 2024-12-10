using System.Diagnostics.CodeAnalysis;

namespace Dto
{
    public class BaseResponse
    {
        [NotNull]
        public int StatusCode { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public BaseResponse(int httpStatusCode, object? data = null, string? message = null)
        {
            Data = data;
            StatusCode = httpStatusCode;
            Message = message;
        }
    }
}

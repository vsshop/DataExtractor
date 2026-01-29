using Delta.Domain.Enums;

namespace Delta.Domain.Implements;

public sealed class Error
{
    public ErrorCode Code { get; set; }
    public string Message { get; set; }
    public IDictionary<string, string[]>? Details { get; set; }

    public Error(ErrorCode code, string message, IDictionary<string, string[]>? details = null)
    {
        Code = code;
        Message = message;
        Details = details;
    }
}
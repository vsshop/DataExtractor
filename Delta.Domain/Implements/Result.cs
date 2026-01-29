using Delta.Domain.Enums;

namespace Delta.Domain.Implements;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Ok() => new(true, null);
    public static Result Fail(Error error) => new(false, error);
}

public sealed class Result<T> : Result
{
    public T? Value { get; }
    private Result(bool isSuccess, T? value, Error? error) : base(isSuccess, error) => Value = value;
    public static Result<T> Ok(T value) => new(true, value, null);
    public static new Result<T> Fail(Error error) => new(false, default, error);
    private static Result<T> Fail(ErrorCode code, string message) => Fail(new Error(code, message));
    public static Result<T> NotFound(string message) => Fail(ErrorCode.NotFound, message);
    public static Result<T> Conflict(string message) => Fail(ErrorCode.Conflict, message);
    public static Result<T> Validation(string message) => Fail(ErrorCode.Validation, message);
    public static Result<T> Forbidden(string message) => Fail(ErrorCode.Forbidden, message);
    public static Result<T> Unauthorized(string message) => Fail(ErrorCode.Unauthorized, message);

    public static implicit operator bool(Result<T> result) => result.IsSuccess;
}

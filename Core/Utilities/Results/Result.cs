using Core.Entities.Abstract;

namespace Core.Utilities.Results;

public class Result : IResult
{
    public bool Success { get; }
    public string Message { get; } = null!;

    protected Result(bool success, string message) : this(success)
    {
        Message = message;
    }

    protected Result(bool success)
    {
        Success = success;
    }
}
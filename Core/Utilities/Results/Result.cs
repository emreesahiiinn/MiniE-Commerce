using Core.Entities.Abstract;

namespace Core.Utilities.Results;

public class Result : IResult
{
    public Result(bool status, string message = null, int statusCode = 200)
    {
        Status = status;
        Message = message;
        StatusCode = statusCode;
    }

    public bool Status { get; }
    public string Message { get; }
    public int StatusCode { get; }
}
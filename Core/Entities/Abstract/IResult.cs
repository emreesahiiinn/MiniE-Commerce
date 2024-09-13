using Newtonsoft.Json;

namespace Core.Entities.Abstract;

public interface IResult
{
    public bool Status { get; }
    public string Message { get; }

    [JsonIgnore] public int StatusCode { get; }
}
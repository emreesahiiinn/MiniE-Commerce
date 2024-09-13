using Newtonsoft.Json;

namespace Core.Extensions;

public class ErrorDetails
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; } = new();
    [JsonIgnore] public int StatusCode { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
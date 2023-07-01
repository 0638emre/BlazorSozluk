using System.Text.Json.Serialization;

namespace BlazorSozluk.Common.Models.Results;

public class ValidationResponseModel
{
    public ValidationResponseModel(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public IEnumerable<string> Errors { get; set; }

    public ValidationResponseModel(string message) : this(new List<string>() { message})
    {
        
    }

    [JsonIgnore]
    public string FlattenErrors => Errors != null ? string.Join(Environment.NewLine, Errors) : string.Empty;

}
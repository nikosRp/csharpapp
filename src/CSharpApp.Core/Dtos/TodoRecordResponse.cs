namespace CSharpApp.Core.Dtos;

public record TodoRecordResponse(
    [property: JsonProperty("userId")] int UserId,
    [property: JsonProperty("id")] int Id,
    [property: JsonProperty("title")] string Title,
    [property: JsonProperty("completed")] bool Completed
);
using Newtonsoft.Json;

using Service.Models.DTOs;

namespace Service.Models.Response;

public class GetBookListResponse
{
    [JsonProperty("books")]
    public ICollection<BookDto> Books { get; set; }
}
using Newtonsoft.Json;

using Service.Models.Data;

namespace Service.Models.Response
{
    public class AddBookResponse
    {
        [JsonProperty("collectionOfIsbns")]
        public IsbnDto[] CollectionOfIsbns { get; set; }
    }
}
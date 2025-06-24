using Newtonsoft.Json;

using Service.Models.Data;

namespace Service.Models.Resquests
{
    public class AddBookRequest
    {
        public AddBookRequest(string userId, string isbn)
        {
            this.UserId = userId;
            CollectionOfIsbns = new IsbnDto[]{new IsbnDto(isbn)};
        }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("collectionOfIsbns")]
        public IsbnDto[] CollectionOfIsbns { get; set; }
    }
}
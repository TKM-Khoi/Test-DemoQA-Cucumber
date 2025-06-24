using Newtonsoft.Json;

namespace Service.Models.DTOs;

public class AccountDto
{
    [JsonProperty("userId")]
    public string UserId { get; set; }
    [JsonProperty("userName")]
    public string Username { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
}
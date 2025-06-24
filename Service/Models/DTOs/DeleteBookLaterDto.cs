namespace Service.Models.DTOs
{
    public class DeleteBookLaterDto
    {
        public string isbn {get;set;} 
        public string userId { get; set; }
        public string userToken { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
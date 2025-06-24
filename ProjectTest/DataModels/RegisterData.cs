using System;
using System.Collections.Generic;
using System.Dynamic;

using Newtonsoft.Json;

namespace ProjectTest.DataModels
{
    public class RegisterData
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("subjects")]
        public ICollection<string> Subjects { get; set; } = new List<string>();
        [JsonProperty("hobbies")]
        public ICollection<string> Hobbies { get; set; } = new List<string>();
        [JsonProperty("picture")]
        public string? Picture { get; set; }
        [JsonProperty("address")]
        public string? Address { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
         public dynamic TrasnformToCustomDynamicObject()
        {
            dynamic obj = new ExpandoObject();
            var dict = (IDictionary<string, object>)obj;

            dict["Student Name"] = FirstName + " " + LastName;
            dict["Student Email"] = Email??"";
            obj.Gender = Gender.ToString();
            obj.Mobile = Phone;
            dict["Date of Birth"] = DateOfBirth.ToString("dd MMM,yyyy");
            obj.Subjects = Subjects==null?"": String.Join(", ", Subjects);
            obj.Hobbies = Hobbies == null? "": String.Join(", ", Hobbies);
            obj.Picture = Picture==null?"": Picture.Substring(Picture.LastIndexOf("\\") + 1);
            obj.Address = Address??"";
            dict["State and City"] = State==null?"":
                State + " " + City??"";
            return obj;
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemiG3.Models
{
    public class Users
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userid")]
        public string UserId { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

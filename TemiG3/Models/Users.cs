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
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("wachtwoord")]
        public string Wachtwoord { get; set; }
    }
}

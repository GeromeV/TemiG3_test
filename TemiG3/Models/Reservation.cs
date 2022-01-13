using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace TemiG3.Models
{
    public class Reservation
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }
        [JsonProperty("groupSize")]
        public int GroupSize { get; set; }

        [JsonProperty("arrivalDate")]
        public string ArrivalDate { get; set; }


        [JsonProperty("arrivalTime")]
        public string ArrivalTime { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("reservationid")]  
        public string ReservationId { get; set; }

    }
}

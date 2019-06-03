using Newtonsoft.Json;
using System;

namespace MovieApp.Model.RequestModel
{
    public class AddPerson
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("date")]
        public string DOB { get; set; }
    }
}

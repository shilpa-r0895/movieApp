using Newtonsoft.Json;
using System;

namespace MovieApp.Model.RequestModel
{
    public class Person
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

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

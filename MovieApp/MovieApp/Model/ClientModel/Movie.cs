using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovieApp.Model.ClientModel
{
    public class Movie
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("year")]
        public int YearOfRelease { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("url")]
        public string PosterURL { get; set; }

        [JsonProperty("producer")]
        public Guid ProducerId { get; set; }

        [JsonProperty("actor")]
        public List<Guid> Actors { get; set; }

    }
}

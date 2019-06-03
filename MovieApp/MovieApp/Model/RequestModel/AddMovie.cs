﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovieApp.Model.RequestModel
{
    public class AddMovie
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("yearOfRelease")]
        public int YearOfRelease { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("posterURL")]
        public string PosterURL { get; set; }

        [JsonProperty("producerId")]
        public Guid ProducerId { get; set; }

        [JsonProperty("actors")]
        public List<Guid> Actors { get; set; }
    }
}

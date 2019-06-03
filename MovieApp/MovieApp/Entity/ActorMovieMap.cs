using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Entity
{
    public class ActorMovieMap
    {

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public Guid MovieId { get; set; }

        [ForeignKey("ActorId")]
        public Person Actor { get; set; }

        public Guid ActorId { get; set; }
    }
}

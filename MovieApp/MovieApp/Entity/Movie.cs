using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Entity
{
    public class Movie
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [Required]
        public string Plot { get; set; }

        [Required]
        public string PosterURL { get; set; }

        [ForeignKey("ProducerId")]
        public Person Producer { get; set; }

        [Required]
        public Guid ProducerId { get; set; }
    }
}

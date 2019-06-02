using MovieApp.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Entity
{
    public class Person
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public string Bio { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public PersonType PersonType { get; set; }
    }
}

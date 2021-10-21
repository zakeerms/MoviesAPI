using MoviesAPI.Validations;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class MovieTheater
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(75)]
        [FirstLetterUppercase]
        public string Name { get; set; }
        public Point Location { get; set; }

    }
}

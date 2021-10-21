using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class MovieTheaterCreationDTO
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(75)]
        [FirstLetterUppercase]
        public string Name { get; set; }
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }

    }
}

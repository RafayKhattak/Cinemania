using Microsoft.AspNetCore.Mvc.Rendering; // Importing the namespace for MVC rendering functionalities
using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations
using System.ComponentModel.DataAnnotations.Schema; // Importing the namespace for database schema annotations

namespace Cinemania.Models.Domain
{
    public class Movie // Defining the Movie class
    {
        public int Id { get; set; } // Property representing the movie's unique identifier

        [Required] // Data annotation specifying that the Title property is required
        public string? Title { get; set; } // Property representing the title of the movie

        public string? ReleaseYear { get; set; } // Property representing the release year of the movie

        public string? MovieImage { get; set; } // Property representing the name of the movie image file

        [Required] // Data annotation specifying that the Cast property is required
        public string? Cast { get; set; } // Property representing the cast of the movie

        [Required] // Data annotation specifying that the Director property is required
        public string? Director { get; set; } // Property representing the director of the movie

        [NotMapped] // Data annotation specifying that the property should not be mapped to a database column
        public IFormFile? ImageFile { get; set; } // Property representing the image file uploaded for the movie

        [NotMapped] // Data annotation specifying that the property should not be mapped to a database column
        [Required] // Data annotation specifying that the Genres property is required
        public List<int>? Genres { get; set; } // Property representing the IDs of genres associated with the movie

        [NotMapped] // Data annotation specifying that the property should not be mapped to a database column
        public IEnumerable<SelectListItem>? GenreList { get; set; } // Property representing the list of genres for dropdown selection

        [NotMapped] // Data annotation specifying that the property should not be mapped to a database column
        public string? GenreNames { get; set; } // Property representing the names of genres associated with the movie

        [NotMapped] // Data annotation specifying that the property should not be mapped to a database column
        public MultiSelectList? MultiGenreList { get; set; } // Property representing the multi-select list of genres for editing
    }
}

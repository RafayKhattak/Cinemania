using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations

namespace Cinemania.Models.Domain
{
    public class MovieGenre // Defining the MovieGenre class
    {
        public int Id { get; set; } // Property representing the unique identifier of the movie-genre relationship

        public int MovieId { get; set; } // Property representing the ID of the movie associated with this relationship

        public int GenreId { get; set; } // Property representing the ID of the genre associated with this relationship
    }
}

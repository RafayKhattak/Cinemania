using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations

namespace Cinemania.Models.Domain
{
    public class Genre // Defining the Genre class
    {
        public int Id { get; set; } // Property representing the genre's unique identifier

        [Required] // Data annotation specifying that the GenreName property is required
        public string? GenreName { get; set; } // Property representing the name of the genre
    }
}

using Cinemania.Models.Domain; // Importing the namespace for domain models

namespace Cinemania.Models.DTO
{
    public class MovieListVm // Defining the MovieListVm class
    {
        public IQueryable<Movie> MovieList { get; set; } // Property representing a list of movies

        public int PageSize { get; set; } // Property representing the size of each page in pagination

        public int CurrentPage { get; set; } // Property representing the current page number

        public int TotalPages { get; set; } // Property representing the total number of pages in pagination

        public string? Term { get; set; } // Property representing the search term used for filtering movies
    }
}

using Cinemania.Models.Domain; // Importing the namespace for domain models
using Cinemania.Models.DTO; // Importing the namespace for DTOs

namespace Cinemania.Repositories.Abstract
{
    public interface IMovieService // Defining the IMovieService interface
    {
        bool Add(Movie model); // Method signature for adding a new movie, accepting a Movie model as input and returning a boolean indicating the success of the operation
        bool Update(Movie model); // Method signature for updating an existing movie, accepting a Movie model as input and returning a boolean indicating the success of the operation
        Movie GetById(int id); // Method signature for retrieving a movie by its unique identifier, accepting the movie ID as input and returning the corresponding Movie object
        bool Delete(int id); // Method signature for deleting a movie by its unique identifier, accepting the movie ID as input and returning a boolean indicating the success of the operation
        MovieListVm List(string term = "", bool paging = false, int currentPage = 0); // Method signature for retrieving a list of movies with optional pagination and search functionality, accepting search term, paging flag, and current page as inputs and returning a MovieListVm object
        List<int> GetGenreByMovieId(int movieId); // Method signature for retrieving the list of genre IDs associated with a movie, accepting the movie ID as input and returning a list of genre IDs
    }
}

using Cinemania.Models.Domain; // Importing the namespace for domain models
using System.Linq; // Importing the namespace for LINQ functionalities

namespace Cinemania.Repositories.Abstract
{
    public interface IGenreService // Defining the IGenreService interface
    {
        bool Add(Genre model); // Method signature for adding a new genre, accepting a Genre model as input and returning a boolean indicating the success of the operation
        bool Update(Genre model); // Method signature for updating an existing genre, accepting a Genre model as input and returning a boolean indicating the success of the operation
        Genre GetById(int id); // Method signature for retrieving a genre by its unique identifier, accepting the genre ID as input and returning the corresponding Genre object
        bool Delete(int id); // Method signature for deleting a genre by its unique identifier, accepting the genre ID as input and returning a boolean indicating the success of the operation
        IQueryable<Genre> List(); // Method signature for retrieving a list of all genres, returning an IQueryable collection of Genre objects
    }
}

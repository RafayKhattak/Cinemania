using Cinemania.Models.Domain; // Importing the namespace for domain models
using Cinemania.Models.DTO; // Importing the namespace for DTOs
using Cinemania.Repositories.Abstract; // Importing the namespace for the abstract movie service
using System; // Importing the namespace for basic utilities
using System.Collections.Generic; // Importing the namespace for generic collections
using System.Linq; // Importing the namespace for LINQ functionalities

namespace Cinemania.Repositories.Implementation
{
    public class MovieService : IMovieService // Implementing the IMovieService interface
    {
        private readonly DatabaseContext ctx; // Database context instance injected via constructor
        public MovieService(DatabaseContext ctx) // Constructor to inject the database context
        {
            this.ctx = ctx; // Assigning the injected database context to the local variable
        }

        public bool Add(Movie model) // Implementation of adding a new movie
        {
            try // Handling potential exceptions
            {
                ctx.Movie.Add(model); // Adding the movie entity to the database context
                ctx.SaveChanges(); // Saving changes to the database

                foreach (int genreId in model.Genres) // Iterating through each selected genre ID
                {
                    var movieGenre = new MovieGenre // Creating a new movie genre entry
                    {
                        MovieId = model.Id, // Assigning the movie ID
                        GenreId = genreId // Assigning the genre ID
                    };
                    ctx.MovieGenre.Add(movieGenre); // Adding the movie genre entry to the database context
                }

                ctx.SaveChanges(); // Saving changes to the database after adding movie genre entries
                return true; // Returning true indicating success
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }

        public bool Delete(int id) // Implementation of deleting a movie by ID
        {
            try // Handling potential exceptions
            {
                var data = this.GetById(id); // Retrieving the movie entity by ID
                if (data == null) // Checking if the movie entity doesn't exist
                    return false; // Returning false if the movie entity doesn't exist

                var movieGenres = ctx.MovieGenre.Where(a => a.MovieId == data.Id); // Retrieving associated movie genres
                foreach (var movieGenre in movieGenres) // Iterating through each associated movie genre
                {
                    ctx.MovieGenre.Remove(movieGenre); // Removing the movie genre from the database context
                }

                ctx.Movie.Remove(data); // Removing the movie entity from the database context
                ctx.SaveChanges(); // Saving changes to the database
                return true; // Returning true indicating success
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }

        public Movie GetById(int id) // Implementation of retrieving a movie by ID
        {
            return ctx.Movie.Find(id); // Finding the movie entity by ID and returning it
        }

        public MovieListVm List(string term = "", bool paging = false, int currentPage = 0) // Implementation of listing movies
        {
            var data = new MovieListVm(); // Creating a new instance of MovieListVm

            var list = ctx.Movie.ToList(); // Querying all movie entities and converting to a list

            if (!string.IsNullOrEmpty(term)) // Checking if search term is provided
            {
                term = term.ToLower(); // Converting search term to lowercase
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList(); // Filtering movies based on title
            }

            if (paging) // Checking if paging is enabled
            {
                int pageSize = 5; // Setting page size
                int count = list.Count; // Getting total count of movies
                int totalPages = (int)Math.Ceiling(count / (double)pageSize); // Calculating total pages
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(); // Applying paging
                data.PageSize = pageSize; // Setting page size in MovieListVm
                data.CurrentPage = currentPage; // Setting current page in MovieListVm
                data.TotalPages = totalPages; // Setting total pages in MovieListVm
            }

            foreach (var movie in list) // Iterating through each movie in the list
            {
                var genres = (from genre in ctx.Genre // Querying genres associated with the movie
                              join mg in ctx.MovieGenre on genre.Id equals mg.GenreId
                              where mg.MovieId == movie.Id
                              select genre.GenreName).ToList();
                var genreNames = string.Join(',', genres); // Combining genre names into a comma-separated string
                movie.GenreNames = genreNames; // Setting genre names in the movie object
            }

            data.MovieList = list.AsQueryable(); // Assigning the list of movies to MovieListVm
            return data; // Returning the MovieListVm object
        }

        public bool Update(Movie model) // Implementation of updating a movie
        {
            try // Handling potential exceptions
            {
                var genresToDelete = ctx.MovieGenre.Where(a => a.MovieId == model.Id && !model.Genres.Contains(a.GenreId)).ToList(); // Finding genres to be deleted
                foreach (var genreToDelete in genresToDelete) // Iterating through genres to be deleted
                {
                    ctx.MovieGenre.Remove(genreToDelete); // Removing genre associations from the database context
                }

                foreach (int genreId in model.Genres) // Iterating through selected genre IDs
                {
                    var movieGenre = ctx.MovieGenre.FirstOrDefault(a => a.MovieId == model.Id && a.GenreId == genreId); // Finding existing genre association
                    if (movieGenre == null) // Checking if genre association doesn't exist
                    {
                        movieGenre = new MovieGenre { GenreId = genreId, MovieId = model.Id }; // Creating a new genre association
                        ctx.MovieGenre.Add(movieGenre); // Adding the new genre association to the database context
                    }
                }

                ctx.Movie.Update(model); // Updating the movie entity in the database context
                ctx.SaveChanges(); // Saving changes to the database
                return true; // Returning true indicating success
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }

        public List<int> GetGenreByMovieId(int movieId) // Implementation of retrieving genre IDs associated with a movie
        {
            var genreIds = ctx.MovieGenre.Where(a => a.MovieId == movieId).Select(a => a.GenreId).ToList(); // Querying genre IDs
            return genreIds; // Returning the list of genre IDs
        }
    }
}

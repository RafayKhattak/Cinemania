using Microsoft.AspNetCore.Mvc; // Importing the MVC namespace for ASP.NET Core functionalities
using Cinemania.Repositories.Abstract; // Importing abstract repository interfaces

namespace Cinemania.Controllers
{
    public class HomeController : Controller // Declaring HomeController as a controller class
    {
        private readonly IMovieService _movieService; // Declaring a private field for the movie service interface

        // Constructor for HomeController, taking IMovieService as a parameter for dependency injection
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService; // Assigning the injected movie service to the private field
        }

        // Action method for the home page (index)
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _movieService.List(term, true, currentPage); // Retrieving a list of movies using the movie service
            return View(movies); // Returning a view with the list of movies
        }

        // Action method to display details of a specific movie
        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movieService.GetById(movieId); // Retrieving a movie by ID using the movie service
            return View(movie); // Returning a view with the movie details
        }
    }
}

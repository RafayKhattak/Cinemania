using Microsoft.AspNetCore.Authorization; // Importing the Authorization namespace for handling authorization
using Microsoft.AspNetCore.Mvc; // Importing the MVC namespace for ASP.NET Core functionalities
using Microsoft.AspNetCore.Mvc.Rendering; // Importing SelectList for generating dropdown lists
using Cinemania.Models.Domain; // Importing custom domain models
using Cinemania.Repositories.Abstract; // Importing abstract repository interfaces

namespace Cinemania.Controllers
{
    [Authorize] // This attribute ensures that users must be authenticated to access any action within this controller
    public class MovieController : Controller // Declaring MovieController as a controller class
    {
        private readonly IMovieService _movieService; // Declaring a private field for the movie service interface
        private readonly IFileService _fileService; // Declaring a private field for the file service interface
        private readonly IGenreService _genService; // Declaring a private field for the genre service interface

        // Constructor for MovieController, taking IGenreService, IMovieService, and IFileService as parameters for dependency injection
        public MovieController(IGenreService genService, IMovieService MovieService, IFileService fileService)
        {
            _movieService = MovieService; // Assigning the injected movie service to the private field
            _fileService = fileService; // Assigning the injected file service to the private field
            _genService = genService; // Assigning the injected genre service to the private field
        }

        // Action method to display the form for adding a new movie
        public IActionResult Add()
        {
            var model = new Movie(); // Creating a new instance of the Movie model
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() }); // Generating a list of genres for dropdown selection
            return View(model); // Returning a view with the model for adding a new movie
        }

        // Action method to handle form submission for adding a new movie
        [HttpPost] // This attribute specifies that this method should handle HTTP POST requests
        public IActionResult Add(Movie model)
        {
            model.GenreList = _genService.List().Select(a => new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() }); // Regenerating the list of genres for dropdown selection
            if (!ModelState.IsValid) // Checking if the model state is not valid
                return View(model); // Returning the same view with validation errors if the model state is not valid

            if (model.ImageFile != null) // Checking if an image file is uploaded
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile); // Saving the image file
                if (fileReult.Item1 == 0) // Checking if the file could not be saved
                {
                    TempData["msg"] = "File could not be saved"; // Setting an error message in TempData
                    return View(model); // Returning the same view with an error message
                }
                var imageName = fileReult.Item2; // Getting the saved image file name
                model.MovieImage = imageName; // Assigning the saved image file name to the movie
            }
            var result = _movieService.Add(model); // Adding the movie using the movie service
            if (result) // If the operation is successful
            {
                TempData["msg"] = "Added Successfully"; // Setting a success message in TempData
                return RedirectToAction(nameof(Add)); // Redirecting to the Add action
            }
            else // If the operation fails
            {
                TempData["msg"] = "Error on server side"; // Setting an error message in TempData
                return View(model); // Returning the same view with an error message
            }
        }

        // Action method to display the form for editing a movie
        public IActionResult Edit(int id)
        {
            var model = _movieService.GetById(id); // Retrieving the movie data by ID using the movie service
            var selectedGenres = _movieService.GetGenreByMovieId(model.Id); // Retrieving the selected genres for the movie
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres); // Generating a multi-select list of genres for editing
            model.MultiGenreList = multiGenreList; // Assigning the multi-select list to the movie model
            return View(model); // Returning a view with the movie data for editing
        }

        // Action method to handle form submission for updating a movie
        [HttpPost]
        public IActionResult Edit(Movie model)
        {
            var selectedGenres = _movieService.GetGenreByMovieId(model.Id); // Retrieving the selected genres for the movie
            MultiSelectList multiGenreList = new MultiSelectList(_genService.List(), "Id", "GenreName", selectedGenres); // Generating a multi-select list of genres for editing
            model.MultiGenreList = multiGenreList; // Assigning the multi-select list to the movie model
            if (!ModelState.IsValid) // Checking if the model state is not valid
                return View(model); // Returning the same view with validation errors if the model state is not valid

            if (model.ImageFile != null) // Checking if an image file is uploaded
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile); // Saving the image file
                if (fileReult.Item1 == 0) // Checking if the file could not be saved
                {
                    TempData["msg"] = "File could not be saved"; // Setting an error message in TempData
                    return View(model); // Returning the same view with an error message
                }
                var imageName = fileReult.Item2; // Getting the saved image file name
                model.MovieImage = imageName; // Assigning the saved image file name to the movie
            }
            var result = _movieService.Update(model); // Updating the movie using the movie service
            if (result) // If the operation is successful
            {
                TempData["msg"] = "Added Successfully"; // Setting a success message in TempData
                return RedirectToAction(nameof(MovieList)); // Redirecting to the MovieList action
            }
            else // If the operation fails
            {
                TempData["msg"] = "Error on server side"; // Setting an error message in TempData
                return View(model); // Returning the same view with an error message
            }
        }

        // Action method to display a list of movies
        public IActionResult MovieList()
        {
            var data = this._movieService.List(); // Retrieving a list of movies using the movie service
            return View(data); // Returning a view with the list of movies
        }

        // Action method to delete a movie
        public IActionResult Delete(int id)
        {
            var result = _movieService.Delete(id); // Deleting the movie by ID using the movie service
            return RedirectToAction(nameof(MovieList)); // Redirecting to the MovieList action
        }
    }
}

using Microsoft.AspNetCore.Authorization; // Importing the Authorization namespace for handling authorization
using Microsoft.AspNetCore.Mvc; // Importing the MVC namespace for ASP.NET Core functionalities
using Cinemania.Models.Domain; // Importing custom domain models
using Cinemania.Repositories.Abstract; // Importing abstract repository interfaces

namespace Cinemania.Controllers
{
    [Authorize] // This attribute ensures that users must be authenticated to access any action within this controller
    public class GenreController : Controller // Declaring GenreController as a controller class
    {
        private readonly IGenreService _genreService; // Declaring a private field for the genre service interface

        // Constructor for GenreController, taking IGenreService as a parameter for dependency injection
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService; // Assigning the injected genre service to the private field
        }

        // Action method to display the form for adding a new genre
        public IActionResult Add()
        {
            return View(); // Returning a view for adding a new genre
        }

        // Action method to handle form submission for adding a new genre
        [HttpPost] // This attribute specifies that this method should handle HTTP POST requests
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid) // Checking if the model state is not valid
                return View(model); // Returning the same view with validation errors if the model state is not valid

            var result = _genreService.Add(model); // Adding the genre using the genre service
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

        // Action method to display the form for editing a genre
        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id); // Retrieving the genre data by ID using the genre service
            return View(data); // Returning a view with the genre data for editing
        }

        // Action method to handle form submission for updating a genre
        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid) // Checking if the model state is not valid
                return View(model); // Returning the same view with validation errors if the model state is not valid

            var result = _genreService.Update(model); // Updating the genre using the genre service
            if (result) // If the operation is successful
            {
                TempData["msg"] = "Added Successfully"; // Setting a success message in TempData
                return RedirectToAction(nameof(GenreList)); // Redirecting to the GenreList action
            }
            else // If the operation fails
            {
                TempData["msg"] = "Error on server side"; // Setting an error message in TempData
                return View(model); // Returning the same view with an error message
            }
        }

        // Action method to display a list of genres
        public IActionResult GenreList()
        {
            var data = this._genreService.List().ToList(); // Retrieving a list of genres using the genre service
            return View(data); // Returning a view with the list of genres
        }

        // Action method to delete a genre
        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id); // Deleting the genre by ID using the genre service
            return RedirectToAction(nameof(GenreList)); // Redirecting to the GenreList action
        }
    }
}

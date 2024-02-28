using Microsoft.AspNetCore.Mvc; // Importing the MVC namespace for ASP.NET Core functionalities
using Cinemania.Models.DTO; // Importing DTO (Data Transfer Object) models
using Cinemania.Repositories.Abstract; // Importing abstract repository interfaces
using System.Threading.Tasks; // Importing Task for asynchronous programming

namespace Cinemania.Controllers
{
    public class UserAuthenticationController : Controller // Declaring UserAuthenticationController as a controller class
    {
        private IUserAuthenticationService authService; // Declaring a private field for the user authentication service

        // Constructor for UserAuthenticationController, taking IUserAuthenticationService as a parameter for dependency injection
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService; // Assigning the injected user authentication service to the private field
        }

        public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "admin",
                Name = "Ravindra",
                Password = "Admin@123",
                PasswordConfirm = "Admin@123",
                Role = "Admin"
            };
            // if you want to register with user, Change Role="User"
            var result = await authService.RegisterAsync(model); // Registering a user with admin rights asynchronously
            return Ok(result.Message); // Returning a response indicating the result of the registration
        }

        // Action method to display the login form
        public async Task<IActionResult> Login()
        {
            return View(); // Returning a view for the login form
        }

        // Action method to handle form submission for user login
        [HttpPost] // This attribute specifies that this method should handle HTTP POST requests
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) // Checking if the model state is not valid
                return View(model); // Returning the same view with validation errors if the model state is not valid

            var result = await authService.LoginAsync(model); // Attempting to log in the user asynchronously
            if (result.StatusCode == 1) // If the login attempt is successful
                return RedirectToAction("Index", "Home"); // Redirecting to the home page
            else // If the login attempt fails
            {
                TempData["msg"] = "Could not log in."; // Setting an error message in TempData
                return RedirectToAction(nameof(Login)); // Redirecting back to the login page
            }
        }

        // Action method to handle user logout
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync(); // Logging out the user asynchronously
            return RedirectToAction(nameof(Login)); // Redirecting to the login page
        }
    }
}

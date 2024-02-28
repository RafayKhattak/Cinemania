using Microsoft.AspNetCore.Identity; // Importing Identity framework namespaces
using Cinemania.Models.Domain; // Importing domain models
using Cinemania.Models.DTO; // Importing DTOs
using Cinemania.Repositories.Abstract; // Importing abstract repository interfaces
using System.Security.Claims; // Importing namespaces for claims
using System.Threading.Tasks; // Importing namespaces for asynchronous operations
using System.Collections.Generic; // Importing namespaces for collections
using System; // Importing namespaces for basic utilities

namespace Cinemania.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService // Implementing the IUserAuthenticationService interface
    {
        private readonly UserManager<ApplicationUser> userManager; // UserManager for managing user-related operations
        private readonly RoleManager<IdentityRole> roleManager; // RoleManager for managing role-related operations
        private readonly SignInManager<ApplicationUser> signInManager; // SignInManager for managing user sign-in operations

        public UserAuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager; // Injecting UserManager instance
            this.roleManager = roleManager; // Injecting RoleManager instance
            this.signInManager = signInManager; // Injecting SignInManager instance
        }

        public async Task<Status> RegisterAsync(RegistrationModel model) // Implementation of user registration
        {
            var status = new Status(); // Creating a Status object to return status information
            var userExists = await userManager.FindByNameAsync(model.Username); // Checking if a user with the same username already exists
            if (userExists != null) // If user exists
            {
                status.StatusCode = 0; // Setting status code to indicate failure
                status.Message = "User already exists"; // Setting status message
                return status; // Returning status
            }

            ApplicationUser user = new ApplicationUser() // Creating a new ApplicationUser object
            {
                Email = model.Email, // Setting email
                SecurityStamp = Guid.NewGuid().ToString(), // Generating a security stamp
                UserName = model.Username, // Setting username
                Name = model.Name, // Setting name
                EmailConfirmed = true, // Setting email confirmed flag
                PhoneNumberConfirmed = true, // Setting phone number confirmed flag
            };
            var result = await userManager.CreateAsync(user, model.Password); // Creating the user
            if (!result.Succeeded) // If user creation fails
            {
                status.StatusCode = 0; // Setting status code to indicate failure
                status.Message = "User creation failed"; // Setting status message
                return status; // Returning status
            }

            if (!await roleManager.RoleExistsAsync(model.Role)) // If the specified role does not exist
                await roleManager.CreateAsync(new IdentityRole(model.Role)); // Creating the role

            if (await roleManager.RoleExistsAsync(model.Role)) // If the specified role exists
                await userManager.AddToRoleAsync(user, model.Role); // Adding the user to the role

            status.StatusCode = 1; // Setting status code to indicate success
            status.Message = "You have registered successfully"; // Setting status message
            return status; // Returning status
        }

        public async Task<Status> LoginAsync(LoginModel model) // Implementation of user login
        {
            var status = new Status(); // Creating a Status object to return status information
            var user = await userManager.FindByNameAsync(model.Username); // Finding the user by username
            if (user == null) // If user not found
            {
                status.StatusCode = 0; // Setting status code to indicate failure
                status.Message = "Invalid username"; // Setting status message
                return status; // Returning status
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password)) // If password is incorrect
            {
                status.StatusCode = 0; // Setting status code to indicate failure
                status.Message = "Invalid password"; // Setting status message
                return status; // Returning status
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, true, true); // Signing in the user
            if (signInResult.Succeeded) // If sign-in is successful
            {
                var userRoles = await userManager.GetRolesAsync(user); // Getting roles assigned to the user
                var authClaims = new List<Claim> // Creating a list of claims for authentication
                {
                    new Claim(ClaimTypes.Name, user.UserName), // Adding username claim
                };

                foreach (var userRole in userRoles) // Adding role claims
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1; // Setting status code to indicate success
                status.Message = "Logged in successfully"; // Setting status message
            }
            else if (signInResult.IsLockedOut) // If user is locked out
            {
                status.StatusCode = 0; // Setting status code to indicate failure
                status.Message = "User is locked out"; // Setting status message
            }
            else // If sign-in fails
            {
                status.StatusCode = 0; // Setting status code to indicate failure
                status.Message = "Error logging in"; // Setting status message
            }

            return status; // Returning status
        }

        public async Task LogoutAsync() // Implementation of user logout
        {
            await signInManager.SignOutAsync(); // Signing out the user
        }
    }
}

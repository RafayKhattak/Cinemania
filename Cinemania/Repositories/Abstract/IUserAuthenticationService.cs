using Cinemania.Models.DTO; // Importing the namespace for DTOs

namespace Cinemania.Repositories.Abstract
{
    public interface IUserAuthenticationService // Defining the IUserAuthenticationService interface
    {
        Task<Status> LoginAsync(LoginModel model); // Method signature for asynchronous login operation, accepting a LoginModel object as input and returning a Status object indicating the outcome of the operation
        Task LogoutAsync(); // Method signature for asynchronous logout operation
        Task<Status> RegisterAsync(RegistrationModel model); // Method signature for asynchronous user registration operation, accepting a RegistrationModel object as input and returning a Status object indicating the outcome of the operation
        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username); // Method signature for asynchronous change password operation, accepting a ChangePasswordModel object and a username as inputs, and returning a Status object indicating the outcome of the operation
    }
}

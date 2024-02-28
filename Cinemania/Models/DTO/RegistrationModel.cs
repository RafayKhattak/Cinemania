using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations

namespace Cinemania.Models.DTO
{
    public class RegistrationModel // Defining the RegistrationModel class
    {
        [Required] // Data annotation specifying that the Name property is required
        public string Name { get; set; } // Property representing the name of the user registering

        [Required] // Data annotation specifying that the Email property is required
        [EmailAddress] // Data annotation specifying that the Email property must be a valid email address
        public string Email { get; set; } // Property representing the email address of the user

        public string Username { get; set; } // Property representing the username chosen by the user

        [Required] // Data annotation specifying that the Password property is required
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; } // Property representing the password chosen by the user

        [Required] // Data annotation specifying that the PasswordConfirm property is required
        [Compare("Password")] // Data annotation specifying that the PasswordConfirm property must match the Password property
        public string PasswordConfirm { get; set; } // Property representing the confirmation of the password entered by the user

        public string Role { get; set; } // Property representing the role of the user (e.g., Admin, User)
    }
}

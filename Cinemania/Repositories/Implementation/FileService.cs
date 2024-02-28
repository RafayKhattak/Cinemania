using Cinemania.Repositories.Abstract; // Importing the namespace for the abstract file service
using Microsoft.AspNetCore.Hosting; // Importing the namespace for hosting environment
using Microsoft.AspNetCore.Http; // Importing the namespace for HTTP features
using System; // Importing the namespace for basic utilities
using System.IO; // Importing the namespace for file-related operations
using System.Linq; // Importing the namespace for LINQ functionalities

namespace Cinemania.Repositories.Implementation
{
    public class FileService : IFileService // Implementing the IFileService interface
    {
        private readonly IWebHostEnvironment environment; // Hosting environment instance injected via constructor
        public FileService(IWebHostEnvironment env) // Constructor to inject the hosting environment
        {
            this.environment = env; // Assigning the injected hosting environment to the local variable
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile) // Implementation of saving image file
        {
            try // Handling potential exceptions
            {
                var wwwPath = this.environment.WebRootPath; // Retrieving the web root path
                var path = Path.Combine(wwwPath, "Uploads"); // Combining web root path with the directory name "Uploads"
                if (!Directory.Exists(path)) // Checking if the directory doesn't exist
                {
                    Directory.CreateDirectory(path); // Creating the directory if it doesn't exist
                }

                var ext = Path.GetExtension(imageFile.FileName); // Getting the extension of the uploaded file
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" }; // Defining allowed extensions
                if (!allowedExtensions.Contains(ext)) // Checking if the uploaded file extension is allowed
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions)); // Creating an error message for disallowed extensions
                    return new Tuple<int, string>(0, msg); // Returning a tuple with status code 0 and the error message
                }

                string uniqueString = Guid.NewGuid().ToString(); // Generating a unique string for the filename
                var newFileName = uniqueString + ext; // Creating the new filename by appending the extension to the unique string
                var fileWithPath = Path.Combine(path, newFileName); // Combining the path with the new filename
                var stream = new FileStream(fileWithPath, FileMode.Create); // Creating a file stream
                imageFile.CopyTo(stream); // Copying the uploaded file to the file stream
                stream.Close(); // Closing the file stream
                return new Tuple<int, string>(1, newFileName); // Returning a tuple with status code 1 (indicating success) and the new filename
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return new Tuple<int, string>(0, "Error has occurred"); // Returning a tuple with status code 0 (indicating failure) and an error message
            }
        }

        public bool DeleteImage(string imageFileName) // Implementation of deleting image file
        {
            try // Handling potential exceptions
            {
                var wwwPath = this.environment.WebRootPath; // Retrieving the web root path
                var path = Path.Combine(wwwPath, "Uploads\\", imageFileName); // Combining web root path with the directory name "Uploads" and the image filename
                if (System.IO.File.Exists(path)) // Checking if the file exists
                {
                    System.IO.File.Delete(path); // Deleting the file
                    return true; // Returning true indicating successful deletion
                }
                return false; // Returning false if the file doesn't exist
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }
    }
}

using Cinemania.Models.Domain; // Importing the namespace for domain models
using Cinemania.Repositories.Abstract; // Importing the namespace for the abstract genre service
using System; // Importing the namespace for basic utilities
using System.Linq; // Importing the namespace for LINQ functionalities

namespace Cinemania.Repositories.Implementation
{
    public class GenreService : IGenreService // Implementing the IGenreService interface
    {
        private readonly DatabaseContext ctx; // Database context instance injected via constructor
        public GenreService(DatabaseContext ctx) // Constructor to inject the database context
        {
            this.ctx = ctx; // Assigning the injected database context to the local variable
        }

        public bool Add(Genre model) // Implementation of adding a new genre
        {
            try // Handling potential exceptions
            {
                ctx.Genre.Add(model); // Adding the genre entity to the database context
                ctx.SaveChanges(); // Saving changes to the database
                return true; // Returning true indicating success
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }

        public bool Delete(int id) // Implementation of deleting a genre by ID
        {
            try // Handling potential exceptions
            {
                var data = this.GetById(id); // Retrieving the genre entity by ID
                if (data == null) // Checking if the genre entity doesn't exist
                    return false; // Returning false if the genre entity doesn't exist
                ctx.Genre.Remove(data); // Removing the genre entity from the database context
                ctx.SaveChanges(); // Saving changes to the database
                return true; // Returning true indicating success
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }

        public Genre GetById(int id) // Implementation of retrieving a genre by ID
        {
            return ctx.Genre.Find(id); // Finding the genre entity by ID and returning it
        }

        public IQueryable<Genre> List() // Implementation of listing all genres
        {
            var data = ctx.Genre.AsQueryable(); // Querying all genre entities
            return data; // Returning the IQueryable collection of genre entities
        }

        public bool Update(Genre model) // Implementation of updating a genre
        {
            try // Handling potential exceptions
            {
                ctx.Genre.Update(model); // Updating the genre entity in the database context
                ctx.SaveChanges(); // Saving changes to the database
                return true; // Returning true indicating success
            }
            catch (Exception ex) // Catching any exceptions that occur during the process
            {
                return false; // Returning false indicating failure
            }
        }
    }
}

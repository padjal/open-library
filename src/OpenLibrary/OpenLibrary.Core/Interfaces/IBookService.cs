using OpenLibrary.Core.Models;

namespace OpenLibrary.Core.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Get books by a specified author query.
        /// </summary>
        /// <param name="author">The search query for the author.</param>
        /// <returns>A collection of books written by the Author. Empty collection if none are found.</returns>
        Task<List<Book>> GetBooksByAuthorAsync(string author);

        /// <summary>
        /// Get books by a specified title query.
        /// </summary>
        /// <param name="title">The specified title query.</param>
        /// <returns>A collection of books written by the Author. Empty collection if none are found.</returns>
        Task<List<Book>> GetBooksByTitleAsync(string title);

    }
}

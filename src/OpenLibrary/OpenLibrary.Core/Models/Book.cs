using Newtonsoft.Json;

namespace OpenLibrary.Core.Models
{
    /// <summary>
    /// Represents a book.
    /// </summary>
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }

        public int PageCount { get; set; }

        public int PublishedYear { get; set; }
    }
}

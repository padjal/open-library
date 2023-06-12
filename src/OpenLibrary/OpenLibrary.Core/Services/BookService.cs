using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenLibrary.Core.Exceptions;
using OpenLibrary.Core.Interfaces;
using OpenLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace OpenLibrary.Core.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(string author)
        {
            try
            {
                var response = await _httpClient.GetAsync($"?author={author}");

                if (response.IsSuccessStatusCode)
                {
                    return await ParseResponseAsync(response);
                }
                else
                {
                    //TODO: Log
                    throw new NotImplementedException();
                }
            }catch(Exception ex)
            {
                // Host error
                throw new HostException(ex.Message, ex);
            }
            
        }

        public async Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            try
            {
                var response = await _httpClient.GetAsync($"?title={title}");

                if (response.IsSuccessStatusCode)
                {
                    return await ParseResponseAsync(response);
                }
                else
                {
                    //TODO: Log
                    throw new NotImplementedException();
                }
            }
            catch(Exception ex)
            {
                // Host error
                throw new HostException(ex.Message, ex);
            }
        }

        private async Task<List<Book>> ParseResponseAsync(HttpResponseMessage response)
        {
            var books = new List<Book>();

            var jsonString = await response.Content.ReadAsStringAsync();

            var data = JObject.Parse(jsonString);

            var booksCollection = data["docs"].Children().ToList();

            foreach (var book in booksCollection)
            {
                try
                {
                    var bookTitle = book["title"].Value<string>();
                    var bookAuthor = (book["author_name"] != null) ? book["author_name"].Children().FirstOrDefault().Value<string>() : "Unknkown";
                    var bookIsnb = (book["isbn"] != null) ? book["isbn"].Children().FirstOrDefault().Value<string>() : "Unknkown";
                    var bookPublishYear = book["first_publish_year"]?.Value<int>();
                    var bookPageCount = book["number_of_pages_median"]?.Value<string>();
                    var bookCoverId = book["cover_i"]?.Value<string>();

                    books.Add(new Book
                    {
                        Title = bookTitle,
                        Author = bookAuthor,
                        PublishedYear = (int)(bookPublishYear == null ? 0 : bookPublishYear),
                        Isbn = bookIsnb,
                        PageCount = bookPageCount == null ? "Not stated" : bookPageCount,
                        CoverImageUrl = bookCoverId != null ? $"https://covers.openlibrary.org/b/id/{bookCoverId}-L.jpg" : "https://i0.wp.com/news.northeastern.edu/wp-content/uploads/2022/12/Books1400.jpg?w=1400&ssl=1"
                    });
                }
                catch (Exception ex)
                {
                    // TODO: Log error
                }

            }

            return books;
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenLibrary.Core.Interfaces;
using OpenLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
            var books = new List<Book>();

             var response = await _httpClient.GetAsync($"?author={author}");

            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var data = JObject.Parse(jsonString);

                var booksCollection = data["docs"].Children().ToList();

                foreach (var book in booksCollection)
                {
                    var bookTitle = book["title"].Value<string>();
                    var bookAuthor = book["author_name"].Children().FirstOrDefault().Value<string>();

                    books.Add(new Book
                    {
                        Title = bookTitle,
                        Author = bookAuthor
                    });
                }
            }
            else
            {
                throw new NotImplementedException();

            }

            return books;
        }

        public Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}

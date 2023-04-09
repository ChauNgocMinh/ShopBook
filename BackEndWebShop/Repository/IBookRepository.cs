using BackEndWebShop.Model;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Repository
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> GetAllBookAsync();
        public Task<BookModel> GetByIdBookAsync(string id);
        public Task<string> AddBookAsync(BookModel model);
        public Task UpDateBookAsync(string id, BookModel model);
        public Task DeleteBookAsync(string id);

    }
}

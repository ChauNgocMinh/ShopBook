﻿using BackEndWebShop.Model;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Repository
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> GetAllBookAsync();
        public Task<BookModel> GetBookByIdAsync(string id);
        public Task<List<BookModel>> GetBookByNameAsync(string NameBook);
        public Task<List<BookModel>> GetByCategoryAsync(string Category);
        public Task<List<BookModel>> GetByPublishingCompanyAsync(string PublishingCompany);
        public Task<string> AddBookAsync(BookModel model);
        public Task UpDateBookAsync(string id, BookModel model);
        public Task DeleteBookAsync(string id);

    }
}

﻿using AutoMapper;
using BackEndWebShop.Data;
using BackEndWebShop.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEndWebShop.Repository
{
    public class BookRepository : IBookRepository
    {
        public readonly IMapper _mapper;
        public readonly BookShopContext _context;

        public BookRepository(IMapper mapper, BookShopContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<string> AddBookAsync(BookModel model)
        {
            var NewBook =  _mapper.Map<Book>(model);
            await _context.Books!.AddAsync(NewBook);
            await _context.SaveChangesAsync();
            return NewBook.Id;
        }

        public async Task DeleteBookAsync(string id)
        {
            var Book = _context.Books!.SingleOrDefault(b => b.Id == id);
            if (Book != null)
            {
                _context.Books!.Remove(Book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookModel>> GetAllBookAsync()
        {
            var ListBook = await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(ListBook);
        }

        public async Task<BookModel> GetBookByIdAsync(string id)
        {
            var Book = await _context.Books!.FindAsync(id);
            return _mapper.Map<BookModel>(Book); 
        }
        public async Task<List<BookModel>> GetBookByNameAsync(string NameBook)
        {
            var Book = await _context.Books!.Where(b => b.Namebook == NameBook).ToListAsync();
            return _mapper.Map<List<BookModel>>(Book);
        }
        public async Task<List<BookModel>> GetByCategoryAsync(string NameBook)
        {
            var Book = await _context.Books!.Where(b => b.Category == NameBook).ToListAsync();
            return _mapper.Map<List<BookModel>>(Book);
        }
        public async Task<List<BookModel>> GetByPublishingCompanyAsync(string PublishingCompany)
        {
            var Book = await _context.Books!.Where(b => b.PublishingCompany == PublishingCompany).ToListAsync();
            return _mapper.Map<List<BookModel>>(Book);
        }
        public async Task UpDateBookAsync(string id, BookModel model)
        {
            if (id == model.Id)
            {
                var updateBook = _mapper.Map<Book>(model);
                _context.Books!.Update(updateBook);
                await _context.SaveChangesAsync();
            }
        }
    }
}

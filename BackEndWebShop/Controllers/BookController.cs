using AutoMapper;
using BackEndWebShop.Data;
using BackEndWebShop.Model;
using BackEndWebShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace BackEndWebShop.Controllers
{
    [Route("Controller/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookRepository _BookRepo;

        public BookController(IBookRepository BookRepo)
        {
            _BookRepo = BookRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _BookRepo.GetAllBookAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetBookById(string id)
        {
            if (id != null)
            {
                return Ok(await _BookRepo.GetBookByIdAsync(id));
            }
            else {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetBookByName(string NameBook)
        {
            if (NameBook != null)
            {
                return Ok(await _BookRepo.GetBookByNameAsync(NameBook));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetBookByCategory(string Category)
        {
            if (Category != null)
            {
                return Ok(await _BookRepo.GetByCategoryAsync(Category));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetBookByPublishingCompany(string PublishingCompany)
        {
            if (PublishingCompany != null)
            {
                return Ok(await _BookRepo.GetByPublishingCompanyAsync(PublishingCompany));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult> AddBook(BookModel model)
        {
            try
            {
                var NewBook = await _BookRepo.AddBookAsync(model);
                var Book = await _BookRepo.GetBookByIdAsync(NewBook);
                return Book == null ? BadRequest() : Ok(Book);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateBook(string id, BookModel model)
        {
            if (id == model.Id)
            {
                await _BookRepo.UpDateBookAsync(id, model);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteBook(string id)
        {
            await _BookRepo.DeleteBookAsync(id);
            return Ok();
        }

    } 
}

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
    [Route("api/[action]")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(string id)
        {
            if (id != null)
            {
                return Ok(await _BookRepo.GetByIdBookAsync(id));
            }
            else {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(BookModel model)
        {
            try
            {
                var NewBook = await _BookRepo.AddBookAsync(model);
                var Book = await _BookRepo.GetByIdBookAsync(NewBook);
                return Book == null ? BadRequest() : Ok(Book);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            await _BookRepo.DeleteBookAsync(id);
            return Ok();
        }

    } 
}

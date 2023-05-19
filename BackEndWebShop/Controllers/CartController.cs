using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BackEndWebShop.Data;
using BackEndWebShop.Repository;
using BackEndWebShop.Model;
using System.Text;

namespace BackEndWebShop.Controllers
{
    [Route("Controller/[action]")]
    [Authorize(Roles = "User")]
    public class CartController : ControllerBase
    {
        public readonly ICartRepository _cartRepo;
        public readonly IBookRepository _bookRepo;
        public readonly BookShopContext _context;

        private readonly Random _random = new Random();
        public CartController(ICartRepository cartRepo, IBookRepository bookRepo)
        {
            _cartRepo = cartRepo;
            _bookRepo = bookRepo;
        }
        [NonAction]
        public string RandomId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder idBuilder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                idBuilder.Append(chars[_random.Next(chars.Length)]);
            }
            return idBuilder.ToString();
        }
        [HttpGet]
        public async Task<IActionResult> ShowCart()
        {
            return Ok(await _cartRepo.ShowCartAsync());
        }
        [HttpPost]
        public async Task<ActionResult> AddItem(string Id, int number)
        {
            try 
            {
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
                var book = await _bookRepo.GetBookByIdAsync(Id);
                CartItemModel item = new CartItemModel
                {
                    Id = RandomId(),
                    Email = email.ToString(),
                    IdBook = Id,
                    Number = number,
                    TotalItem = number * book.Price,
                    Status = true,
                    Date = DateTime.Now,
                };
                await _cartRepo.AddItemAsync(item);
                return Ok();
            }
            catch 
            {
                return NotFound();
            }
           
        }
        [HttpPost]
        public async Task<ActionResult> Buy(string Id)
        {
            try
            {
                var Item = await _cartRepo.GetCartByIdAsync(Id);
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

                var NewBill = new BillModel
                {
                    Id = RandomId(),
                    Email = email,
                    IdCartItem = Id,
                    BuyingDate = DateTime.Now,
                };
                await _cartRepo.BuyAsync(Id, NewBill);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<ActionResult> BillHistory()
        {
            await _cartRepo.BillHistoryAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleleItemCart(string Id)
        {
            try
            {
                await _cartRepo.RemoveItenAsync(Id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditNumberItemCart(string IdItem,int number)
        {
            try
            {
                await _cartRepo.EditNumberItemAsync(IdItem, number);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

    }
}

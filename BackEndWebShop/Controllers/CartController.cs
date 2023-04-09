using BackEndWebShop.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndWebShop.Model;
using BackEndWebShop.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BackEndWebShop.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly BookShopContext _context;

        public CartController(BookShopContext context)
        {
            _context = context;
        }

        public List<CartBookModel> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartBookModel>>("GioHang");
                if (data == null)
                {
                    data = new List<CartBookModel>();
                }
                return data;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ShowCart()
        {
            return Ok(Carts);
        }

        [HttpPost]
        
        public async Task<IActionResult> AddToCart(string id, int Quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == id);

            if (item == null)//chưa có
            {
                var book = _context.Books!.SingleOrDefault(p => p.Id == id);
                item = new CartBookModel
                {
                    Id = id,
                    Namebook = book.Namebook,
                    Quantity = Quantity,
                    Category = book.Category,
                    Price = book.Price,
                };
                myCart.Add(item);
            }
            else
            {
                item.Quantity += Quantity;
            }
            HttpContext.Session.Set("GioHang", myCart);

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == id);
            if (item == null)//chưa có
            {
                return BadRequest();
            }
            else
            {
                myCart.Remove(item);
            }
            HttpContext.Session.Set("GioHang", myCart);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> EditCart(string id, int Quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.Id == id);

            if (item == null)//chưa có
            {
                return BadRequest();
            }
            else
            {
                item.Quantity = Quantity;
            }
            HttpContext.Session.Set("GioHang", myCart);

            return Ok();
        }

    }
}

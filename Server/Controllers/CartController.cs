using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Cart;

namespace Server.Controllers
{
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartservice;
        public CartController(ICartService cartservice)
        {
            _cartservice = cartservice;
        }
        [HttpPost]
        public ActionResult AddGiftToCart(int giftId, int  qentity)
        {
            _cartservice.AddGiftToCart(giftId, qentity);
            return Ok();
        }
    }
}

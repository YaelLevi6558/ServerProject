using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Gift;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftservice;
        public GiftController(IGiftService giftservice)
        {
            _giftservice = giftservice;
        }
        [Authorize(Roles = "User, Admin")]

        [HttpGet]
        public IActionResult GetAllGifts() {
            var allGift = _giftservice.GetAllGifts();
            return Ok(allGift);
        }
        [Authorize(Roles = "User, Admin")]
        [HttpGet("get by name")]
        public IActionResult GetGiftByName(string name)
        {
            var newGift = _giftservice.GetGiftByName(name);
            return Ok(newGift);
        }

        [HttpGet("get by donor name")]
        public IActionResult GetGiftByDonorName(string donorName)
        {
            var listGifts = _giftservice.GetGiftsByDonorName(donorName);
            return Ok(listGifts);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddGift([FromBody] Models.GiftConnection gift)
        {
            _giftservice.AddGift(gift);
            return CreatedAtAction(nameof(GetAllGifts), new { gift.GiftId }, gift);   
            //try
            //{
            //    _giftservice.AddGift(gift);
            //    return CreatedAtAction(nameof(GetAllGifts), new { gift }, gift);
            //}
            //catch (Exception ex) 
            //{
            //    return BadRequest(ex.Message);
            //}
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult UpdateGift([FromBody] Models.GiftConnection gift) 
        {
            _giftservice.UpdateGift(gift);

            return Ok();
        }
        [Authorize(Roles = "Admin")]

        [HttpDelete]
        public ActionResult DeleteGift(int id) 
        {
            _giftservice.DeleteGift(id);     
            return Ok("מתנה נמחקה");
        }
        [Authorize(Roles = "User")]
        [HttpGet("OrderByPrice")]
        public IActionResult OrderByPrice()
        {
            var byPrice = _giftservice.OrderByPrice();
            return Ok(byPrice);
        }
        [Authorize(Roles = "User")]
        [HttpGet("OrderByCategory")]
        public IActionResult OrderByCategory(string category)
        {
            var byCategory = _giftservice.OrderByCategory(category);
            return Ok(byCategory);
        }
    }
}

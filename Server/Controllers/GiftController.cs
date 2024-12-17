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
        [HttpGet]
        public IActionResult GetAllGifts() {
            var allGift = _giftservice.GetAllGifts();
            return Ok(allGift);
        }
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
        [HttpPut]
        public ActionResult UpdateGift([FromBody] Models.GiftConnection gift) 
        {
            _giftservice.UpdateGift(gift);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteGift(int id) 
        {
            _giftservice.DeleteGift(id);     
            return Ok("מתנה נמחקה");
        }
    }
}

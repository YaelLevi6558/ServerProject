using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Winner;

namespace Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerService _winnerService;
        public WinnerController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }
        [HttpGet]
        public ActionResult RandomToGift() 
        { 
            _winnerService.RandomToGift();
            return Ok();

        }
        [HttpGet("giftId")]
        public ActionResult RandomToGift(int id)
        {
            _winnerService.RandomToGift(id);
            return Ok();
        }
        [HttpGet("excel")]
        public ActionResult GenerateExcelReport()
        {
            _winnerService.GenerateExcelReport();
            return Ok();
        }
               
    }
}

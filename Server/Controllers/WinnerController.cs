﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Winner;

namespace Server.Controllers
{
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
    }
}
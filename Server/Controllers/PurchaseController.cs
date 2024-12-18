using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.Gift;
using Server.Services.IPurchaseService;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchasesservice;
        public PurchaseController(IPurchaseService purchasesservice)
        {
            _purchasesservice = purchasesservice;
        }
        [HttpGet("GetPurchaseByGift")]
        public IActionResult GetPurchaseByGift(string giftName) 
        { 
            var p = _purchasesservice.GetPurchaseByGift(giftName);
            return Ok(p);   
        }
        [HttpGet("getPurchases")]
        public IActionResult GetPurchases()
        {
            var p = _purchasesservice.GetPurchases();
            return Ok(p);
        }
        [HttpGet("OrderByExpensiveGift")]
        public IActionResult OrderByExpensiveGift()
        {
            var orderBy = _purchasesservice.OrderByExpensiveGift();
            return Ok(orderBy); 
        }
        [HttpGet("OrderByAmountTicket")]
        public IActionResult OrderByAmountTicket()
        {
            var orderBy = _purchasesservice.OrderByAmountTicket();
            return Ok(orderBy);
        }
    }
}

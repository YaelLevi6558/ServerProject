using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.Donor;

namespace Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorservice;
        public DonorController(IDonorService donorservice)
        {
            _donorservice = donorservice;
        }
       
        [HttpGet]
        public IActionResult getAllDonors()
        {
            var donors = _donorservice.GetAllDonors();
            return Ok(donors);
        }
        [HttpGet("getDonorGifts")]
        public IActionResult getDonorGifts(int id) 
        {
            var donor = _donorservice.GetDonorGifts(id);
            return Ok(donor);
        }


        [HttpPost]
        public ActionResult addDonor([FromBody] Models.Donor donor) 
        { 
            _donorservice.AddDonor(donor);
            return CreatedAtAction(nameof(getAllDonors), new { donor.DonorId }, donor);
        }
        [HttpPut]
        public ActionResult updateDonor([FromBody] Models.Donor donor)
        {
            _donorservice.UpdateDonor(donor);
            return Ok(donor);
        }
        [HttpDelete]
        public ActionResult deleteDOnor(int id)
        {
            _donorservice.DeleteDonor(id);
            return Ok(); 
        }
    }
}

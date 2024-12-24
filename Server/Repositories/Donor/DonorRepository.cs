
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Diagnostics.Eventing.Reader;

namespace Server.Repositories.Donor
{
    public class DonorRepository : IDonorRepository
    {
        private readonly ChineseAuctionContext _context;
        public DonorRepository(ChineseAuctionContext context)
        {
            _context = context; 
        }
        public List<Models.Donor> GetAllDonors()
        {
            var allDonors = _context.Donors.ToList();
            return allDonors;
        }

        public void AddDonor(Models.Donor donor)
        {
           _context.Donors.Add(donor);
           _context.SaveChanges();
        }

        public void DeleteDonor(int id)
        {
            var deleteDonor = _context.Donors.Include(x => x.Gifts).FirstOrDefault(x => x.DonorId == id);
            if (deleteDonor == null) 
            {
                throw new Exception("תורם עם ID זה לא נמצא");
            }
            _context.Gifts.RemoveRange(deleteDonor.Gifts);
            _context.Donors.Remove(deleteDonor);
            _context.SaveChanges();
        }
        public void UpdateDonor(Models.Donor donor)
        {
            _context.Donors.Update(donor);
            _context.SaveChanges();
        }
        public List<Models.Gift> GetDonorGifts(int id)
        {
            var GetDG = _context.Donors
                .Where(d => d.DonorId == id)
                .Select(d => d.Gifts)
                .FirstOrDefault();
            if (GetDG == null)
            {
                return new List<Models.Gift>();
                throw new Exception("לא נמצא תורם עם ID זה");
            }
            return GetDG.ToList();
            
        }
    }
}


using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Server.IIS;
using Server.Models;

namespace Server.Repositories.Gift
{
    public class GiftRepository : IGiftRepository
    {
        private readonly ChineseAuctionContext _context;
        public GiftRepository(ChineseAuctionContext context)
        {
            _context = context;
        }
        public List<Models.Gift> GetAllGifts()
        {
            var allGifts = _context.Gifts.ToList();
            return allGifts;
        }
        public void AddGift(Models.Gift gift)
        {
            if (gift == null)
            {
                throw new Exception("gift is null");
            }    
            var caterory = _context.Categories.FirstOrDefault(x => x.CategoryId == gift.CategoryId);
            var donor = _context.Donors.FirstOrDefault(x=> x.DonorId == gift.DonorId);
            if (caterory != null && donor != null)
            {
                gift.Category = caterory;
                gift.Donor = donor;
            }
            else
            {
                throw new Exception("Invalid Category or Donor.");
            }
            _context.Gifts.Add(gift);
            _context.SaveChanges();
        }
        public void UpdateGift(Models.Gift gift)
        {
            var id = _context.Gifts.Find(gift.GiftId);
            //if (id == null)
            //{
            //    throw new Exception("לא נמצא ID מתאים");
            //}
            _context.Gifts.Update(gift);
            _context.SaveChanges();
        }
        public void DeleteGift(int id)
        {
            var deleteGift = _context.Gifts.Find(id);
            if (deleteGift != null)
            {
                _context.Gifts.Remove(deleteGift);
                _context.SaveChanges();
            }
        }
    
    
    }
}

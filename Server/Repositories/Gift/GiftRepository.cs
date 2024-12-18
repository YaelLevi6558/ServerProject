
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
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
        public List<Models.GiftConnection> GetAllGifts()
        {
            var giftList = (from gift in _context.Gifts
                            join category in _context.Categories on gift.CategoryId equals category.CategoryId
                            join donor in _context.Donors on gift.DonorId equals donor.DonorId
                            select new Models.GiftConnection
                            {
                                GiftId = gift.GiftId,
                                GiftName = gift.GiftName,
                                CategoryName = category.CategoryName,
                                DonorName = donor.FirstName,
                                TicketCost = gift.TicketCost,
                                ImageUrl = gift.ImageUrl
                            }
                            ).ToList();
            return giftList;

            //var allGifts = _context.Gifts.ToList();
            //List<Models.GiftConnection> giftList = new List<GiftConnection>();
            //foreach (var allGift in allGifts)
            //{
            //    var caterory = _context.Categories.FirstOrDefault(x => x.CategoryId == allGift.CategoryId);
            //    var donor = _context.Donors.FirstOrDefault(x => x.DonorId == allGift.DonorId);
            //    var giftNew = new Models.GiftConnection();
            //    giftNew.GiftId = allGift.GiftId;
            //    giftNew.GiftName = allGift.GiftName;
            //    giftNew.CategoryName = caterory.CategoryName;
            //    giftNew.DonorName = donor.FirstName;
            //    giftNew.TicketCost = allGift.TicketCost;
            //    giftNew.ImageUrl = allGift.ImageUrl;
            //    giftList.Add(giftNew);
            //}
            //return giftList;
        }
        public void AddGift(Models.GiftConnection gift)
        {
            if (gift == null)
            {
                throw new Exception("gift is null");
            }


            var caterory = _context.Categories.FirstOrDefault(x => x.CategoryName == gift.CategoryName);
            var donor = _context.Donors.FirstOrDefault(x => x.FirstName == gift.DonorName);
            if (caterory == null || donor == null)
            {
                throw new Exception("Category or Donor not found");
            }
            var giftNew = new Models.Gift
            {
                GiftName = gift.GiftName,
                CategoryId = caterory.CategoryId,
                DonorId = donor.DonorId,
                TicketCost = gift.TicketCost,
                ImageUrl = gift.ImageUrl
            };

            _context.Gifts.Add(giftNew);
            _context.SaveChanges();
        }
        public void UpdateGift(Models.GiftConnection gift)
        {
            var caterory = _context.Categories.FirstOrDefault(x => x.CategoryName == gift.CategoryName);
            var donor = _context.Donors.FirstOrDefault(x => x.FirstName == gift.DonorName);
            var g = _context.Gifts.Find(gift.GiftId);           
            g.GiftName = gift.GiftName;
            g.CategoryId = caterory.CategoryId;
            g.DonorId = donor.DonorId;
            g.TicketCost = gift.TicketCost;
            g.ImageUrl = gift.ImageUrl;
            _context.Gifts.Update(g);
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

        public GiftConnection GetGiftByName(string name)
        {
            var newGift = (from g in _context.Gifts
                           join category in _context.Categories on g.CategoryId equals category.CategoryId
                           join donor in _context.Donors on g.DonorId equals donor.DonorId
                           where g.GiftName == name
                           select new Models.GiftConnection
                           {
                               GiftId = g.GiftId,
                               GiftName = g.GiftName,
                               CategoryName = category.CategoryName,
                               DonorName = donor.FirstName,
                               TicketCost = g.TicketCost,
                               ImageUrl = g.ImageUrl
                           }).FirstOrDefault();
            return newGift;
        }

        public List<GiftConnection> GetGiftsByDonorName(string donorName)
        {
            var donor = _context.Donors.FirstOrDefault(x => x.FirstName == donorName);
            var gift = _context.Gifts.Where(x => x.DonorId == donor.DonorId).ToList();
            List<Models.GiftConnection> giftList = new List<Models.GiftConnection>();
            foreach (var item in gift)
            {
                var caterory = _context.Categories.FirstOrDefault(x => x.CategoryId == item.CategoryId);
                var newGift = new GiftConnection();
                newGift.GiftId = item.GiftId;
                newGift.GiftName = item.GiftName;
                newGift.CategoryName = caterory.CategoryName;
                newGift.DonorName = donor.FirstName;
                newGift.TicketCost = item.TicketCost;
                newGift.ImageUrl = item.ImageUrl;
                giftList.Add(newGift);
            }
            return giftList;
        }
    }
    //public List<GiftConnection> GetGiftsByDonorName(string donorName)
    //{
    //    if (string.IsNullOrEmpty(donorName))
    //    {
    //        return new List<GiftConnection>();
    //    }
    //    var giftList = (from gift in _context.Gifts
    //                    join category in _context.Categories on gift.CategoryId equals category.CategoryId
    //                    join donor in _context.Donors on gift.DonorId equals donor.DonorId
    //                    where donor.FirstName == donorName
    //                    select new GiftConnection
    //                    {
    //                        GiftId = gift.GiftId,
    //                        GiftName = gift.GiftName,
    //                        CategoryName = category.CategoryName,
    //                        DonorName = donor.FirstName,
    //                        TicketCost = gift.TicketCost,
    //                        ImageUrl = gift.ImageUrl
    //                    }).ToList();
    //    return giftList;
    //}

}

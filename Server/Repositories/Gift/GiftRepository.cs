
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
            if (string.IsNullOrEmpty(donorName))
            {
                return new List<GiftConnection>();
            }
            var giftList = (from gift in _context.Gifts
                            join category in _context.Categories on gift.CategoryId equals category.CategoryId
                            join donor in _context.Donors on gift.DonorId equals donor.DonorId
                            where donor.FirstName == donorName
                            select new GiftConnection
                            {
                                GiftId = gift.GiftId,
                                GiftName = gift.GiftName,
                                CategoryName = category.CategoryName,
                                DonorName = donor.FirstName,
                                TicketCost = gift.TicketCost,
                                ImageUrl = gift.ImageUrl
                            }).ToList();
            return giftList;
        }

        public List<GiftConnection> OrderByPrice()
        {
            var byPrice  = (from g in _context.Gifts
                            join d in _context.Donors on g.DonorId equals d.DonorId
                            join c in _context.Categories on g.CategoryId equals c.CategoryId
                            orderby g.TicketCost descending
                            select new GiftConnection
                            {
                                GiftId = g.GiftId,
                                GiftName = g.GiftName,
                                CategoryName = c.CategoryName,
                                DonorName = d.FirstName,
                                TicketCost = g.TicketCost,
                                ImageUrl= g.ImageUrl
                            }).ToList();
            return byPrice;
        }

        public List<GiftConnection> OrderByCategory(string category)
        {
            var byCategory = (from g in _context.Gifts
                           join d in _context.Donors on g.DonorId equals d.DonorId
                           join c in _context.Categories on g.CategoryId equals c.CategoryId
                           orderby c.CategoryName == category descending
                           select new GiftConnection
                           {
                               GiftId = g.GiftId,
                               GiftName = g.GiftName,
                               CategoryName = c.CategoryName,
                               DonorName = d.FirstName,
                               TicketCost = g.TicketCost,
                               ImageUrl = g.ImageUrl
                           }).ToList();
            return byCategory;
        }

    }
}

using Server.Models;
using System.Reflection;

namespace Server.Repositories.Winner
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly ChineseAuctionContext _context;
        public WinnerRepository(ChineseAuctionContext context)
        {
            _context = context;
        }
        public void RandomToGift()
        {
            var random = new Random();

            var gifts = (from g in _context.Gifts
                         join p in _context.Purchases on g.GiftId equals p.GiftId
                         join u in _context.Users on p.UserId equals u.UserId
                         group new { p, u } by new { g.GiftId, g.GiftName } into giftGroup
                         select new
                         {
                             giftId = giftGroup.Key.GiftId,
                             giftName = giftGroup.Key.GiftName,
                             winners = giftGroup.ToList()
                         }).ToList();
            foreach (var gift in gifts)
            {
                if (gift.winners.Count == 0)
                {
                    continue;
                }
                var winner = gift.winners.OrderBy(x => random.Next()).FirstOrDefault();

                var WINNER = new Models.Winner
                {
                    GiftId = gift.giftId,
                    WinnerName = winner.u.UserFirstName,
                    WinnerEmail = winner.u.UserEmail,
                    WinnerPhone = winner.u.UserPhone,
                    PurchaseId = winner.p.PurchaseId,
                    WinningDate = DateTime.Now
                };
                _context.Winners.Add(WINNER);
            }
            _context.SaveChanges();
        }
    }
}

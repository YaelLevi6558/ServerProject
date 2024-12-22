using OfficeOpenXml;
using Server.Models;

namespace Server.Repositories.Purchase
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ChineseAuctionContext _context;
        public PurchaseRepository(ChineseAuctionContext context)
        {
            _context = context;
        }
        public List<Models.PurchaseDetails> GetPurchaseByGift(string giftName)
        {
            var purchases = (from p in _context.Purchases
                        join g in _context.Gifts on p.GiftId equals g.GiftId
                        join u in _context.Users on p.UserId equals u.UserId
                        where g.GiftName == giftName
                        select new PurchaseDetails
                        {
                           PurchaseId = p.PurchaseId,
                           GiftName = giftName,
                           UserName = u.UserFirstName,
                           PurchaseDate = p.PurchaseDate,
                           NumberOfTickets = p.NumberOfTickets
                        }).ToList();
            return purchases;
        } 
        List<PurchaseDetails> IPurchaseRepository.GetPurchases()
        {        
            var purchases = (from p in _context.Purchases
                             join u in _context.Users on p.UserId equals u.UserId
                             join g in _context.Gifts on p.GiftId equals g.GiftId
                             select new PurchaseDetails
                             {
                                 PurchaseId = p.PurchaseId,
                                 GiftName = g.GiftName,
                                 UserName = u.UserName,
                                 UserFirstName = u.UserFirstName,
                                 UserLastName = u.UserLastName,
                                 UserEmail = u.UserEmail,
                                 UserPhone = u.UserPhone,
                                 NumberOfTickets = p.NumberOfTickets,
                                 PurchaseDate = p.PurchaseDate
                             }).ToList();
            return purchases;
        }
        public List<PurchaseDetails> OrderByExpensiveGift()
        {
            var byExpensiveGift = (from p in _context.Purchases
                                   join u in _context.Users on p.UserId equals u.UserId
                                   join g in _context.Gifts on p.GiftId equals g.GiftId
                                   orderby g.TicketCost descending
                                   select new PurchaseDetails
                                   {
                                       PurchaseId = p.PurchaseId, 
                                       GiftName = g.GiftName,
                                       UserName = u.UserName,
                                       UserFirstName = u.UserFirstName,
                                       UserLastName = u.UserLastName,
                                       UserEmail = u.UserEmail,
                                       NumberOfTickets = p.NumberOfTickets,
                                       PurchaseDate = p.PurchaseDate,
                                       TicketCost = g.TicketCost

                                   }
                                   ).ToList();
            return byExpensiveGift;
        }
        public List<PurchaseDetails> OrderByAmountTicket()
        {       
            var byAmountTicket = (from p in _context.Purchases
                                  join g in _context.Gifts on p.GiftId equals g.GiftId
                                  group p by new {g.GiftId, g.GiftName, g.TicketCost} into groupGift
                                  orderby groupGift.Sum(x=> x.NumberOfTickets) descending
                                  select new PurchaseDetails
                                  {                                     
                                      GiftName = groupGift.Key.GiftName,
                                      NumberOfTickets = groupGift.Sum(x => x.NumberOfTickets),
                                      TicketCost = groupGift.Key.TicketCost
                                  }).ToList();
            return byAmountTicket;
        }

        public void GenerateSalesRevenueReport()
        {
            var purchase = _context.Purchases.ToList();
            using (var package = new ExcelPackage())
            {
                decimal count = 0;
                var worksheet = package.Workbook.Worksheets.Add("Purchase Report");
                worksheet.Cells[1, 1].Value = "Total Revenue";
                int row = 2;
                foreach (var p in purchase)
                {
                    var gift = (from g in _context.Gifts
                                where g.GiftId == p.GiftId
                                select g).FirstOrDefault();
                    
                    count += p.NumberOfTickets * gift.TicketCost;
                    worksheet.Cells[row, 1].Value = count;

                }

                var filePath = "PurchaseReport.xlsx";
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }
        }
    }
}

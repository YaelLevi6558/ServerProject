using OfficeOpenXml;
using Server.Models;
using System.Net.Mail;
using System.Net;
using System.Reflection;
namespace Server.Repositories.Winner
{

    public class WinnerRepository : IWinnerRepository
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "your-email@gmail.com"; // כתובת אימייל שולח
        private readonly string _smtpPassword = "your-app-password"; // סיסמת אפליקציה או סיסמת חשבון
        private readonly bool _enableSsl = true;
        private readonly ChineseAuctionContext _context;
        public WinnerRepository(ChineseAuctionContext context)
        {
            _context = context;
        }
        public void RandomToGift()
        {
            Console.WriteLine($"Gifts count: {_context.Gifts.Count()}");
            Console.WriteLine($"Purchases count: {_context.Purchases.Count()}");
            Console.WriteLine($"Users count: {_context.Users.Count()}");
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
                if (gift.winners == null || gift.winners.Count == 0)
                {
                    continue;
                }

                var winner = gift.winners.OrderBy(x => random.Next()).FirstOrDefault();
                if (winner == null || winner.u == null || winner.p == null)
                {
                    continue;
                }
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
        public void RandomToGift(int id)
        {

            var random = new Random();

            var gifts = (from g in _context.Gifts
                         join p in _context.Purchases on g.GiftId equals p.GiftId
                         join u in _context.Users on p.UserId equals u.UserId
                         where g.GiftId == id
                         group new { p, u } by new { g.GiftId, g.GiftName } into giftGroup
                         select new
                         {
                             giftId = giftGroup.Key.GiftId,
                             giftName = giftGroup.Key.GiftName,
                             winners = giftGroup.ToList()
                         }).ToList();

            foreach (var gift in gifts)
            {
                if (gift.winners == null || gift.winners.Count == 0)
                {
                    continue;
                }
                var winner = gift.winners.OrderBy(x => random.Next()).FirstOrDefault();
                if (winner == null || winner.u == null || winner.p == null)
                {
                    continue;
                }
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
               // SendWinnerEmail(winner.u.UserEmail, winner.u.UserFirstName, gift.giftName);

            }
            _context.SaveChanges();
        }
        public void GenerateExcelReport()
        {
            var winners = _context.Winners.ToList();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Winners Report");
                worksheet.Cells[1, 1].Value = "Gift Name";
                worksheet.Cells[1, 2].Value = "Winner Name";
                int row = 2;
                foreach (var winner in winners)
                {
                    var gift = (from g in _context.Gifts
                               where g.GiftId == winner.GiftId
                               select g).FirstOrDefault();
                    worksheet.Cells[row, 1].Value = gift.GiftName;
                    worksheet.Cells[row, 2].Value = winner.WinnerName;
                    row++;
                }
                var filePath = "WinnersReport.xlsx";
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
            }

        }

        //public async void SendWinnerEmail(string toEmail, string subject, string body)
        //{
        //    try
        //    {
        //        var smtpClient = new SmtpClient(_smtpHost)
        //        {
        //            Port = _smtpPort,
        //            Credentials = new NetworkCredential(_smtpUser, _smtpPassword),
        //            EnableSsl = _enableSsl,
        //        };

        //        var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress(_smtpUser),
        //            Subject = subject,
        //            Body = body,
        //            IsBodyHtml = false,
        //        };

        //        mailMessage.To.Add(toEmail); // כתובת המייל של הזוכה

        //        await smtpClient.SendMailAsync(mailMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        // טיפול בשגיאות במקרה של כשלון בשליחת המייל
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}
    }
}

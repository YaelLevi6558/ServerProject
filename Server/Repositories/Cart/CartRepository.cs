using Server.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Repositories.Cart
{
    public class CartRepository:ICartRepository
    {
        public List<Models.Gift> Gifts { get; set; } = new List<Models.Gift>();
        private readonly ChineseAuctionContext _context;       
        public CartRepository(ChineseAuctionContext context)
        {
            _context = context;           
        }      
        public void AddGiftToCart(int giftId, int qentity)
        {
           
            if(qentity <= 0)
            {
                throw new ArgumentOutOfRangeException(" כמות לא תקינה");
            }
            var gift = _context.Gifts.FirstOrDefault(x => x.GiftId == giftId);
           // var userId = GetUserIdFromToken(); // פונקציה שמחזירה את מזהה המשתמש מתוך הטוקן
         
            if (gift == null)
            {
                throw new ArgumentException("מתנה לא נמצאה");
            }
            var cart = _context.Carts.FirstOrDefault(x=> x.GiftId == giftId);
            if (cart != null)
            {
                cart.NumberOfTickets += qentity;
                cart.CaertDate = DateTime.Now;                
            }
            else
            {
                var newItemInCart = new Models.Cart
                {
                    UserId = 0,
                    GiftId = gift.GiftId,
                    NumberOfTickets = qentity,
                    CaertDate = DateTime.Now
                };
                _context.Carts.Add(newItemInCart);
            }
            _context.SaveChanges();
           

        }
    }
}

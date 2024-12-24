using Server.Repositories.Cart;

namespace Server.Services.Cart
{
    public class CartService:ICartService
    {
        private readonly ICartRepository _cartrepository;
        public CartService(ICartRepository cartrepository)
        {
            _cartrepository = cartrepository;
        }

        public void AddGiftToCart(int giftId, int qentity)
        {
            _cartrepository.AddGiftToCart(giftId, qentity);
        }
    }
}

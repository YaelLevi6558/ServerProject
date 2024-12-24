namespace Server.Repositories.Cart
{
    public interface ICartRepository
    {
        void AddGiftToCart(int giftId, int qentity);
    }
}

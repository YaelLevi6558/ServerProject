using Server.Models;

namespace Server.Services.Gift
{
    public interface IGiftService
    {
        List<Models.GiftConnection> GetAllGifts();
        void AddGift(Models.GiftConnection gift);
        void UpdateGift(Models.GiftConnection gift);
        void DeleteGift(int id);
        Models.GiftConnection GetGiftByName(string name);
        List<Models.GiftConnection> GetGiftsByDonorName(string donorName);
        List<GiftConnection> OrderByPrice();
        List<GiftConnection> OrderByCategory(string category);
    }
}

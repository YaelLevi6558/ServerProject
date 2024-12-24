using Server.Models;
namespace Server.Repositories.Gift

{
    public interface IGiftRepository
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

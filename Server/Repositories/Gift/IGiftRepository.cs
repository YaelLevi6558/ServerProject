using Server.Models;
namespace Server.Repositories.Gift

{
    public interface IGiftRepository
    {
        List<Models.Gift> GetAllGifts();
        void AddGift(Models.Gift gift);
        void UpdateGift(Models.Gift gift);
        void DeleteGift(int id);
    }
}

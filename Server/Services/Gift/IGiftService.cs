namespace Server.Services.Gift
{
    public interface IGiftService
    {
        List<Models.Gift> GetAllGifts();
        void AddGift(Models.Gift gift);
        void UpdateGift(Models.Gift gift);
        void DeleteGift(int id);
    }
}

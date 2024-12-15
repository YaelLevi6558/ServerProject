using Server.Repositories;
using Server.Repositories.Gift;
namespace Server.Services.Gift
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _giftrepository;
        public GiftService(IGiftRepository giftrepository)
        {
            _giftrepository = giftrepository;
        }

        public List<Models.Gift> GetAllGifts()
        {
            var allGifts = _giftrepository.GetAllGifts();
            if (allGifts == null) 
            { 
                 return new List<Models.Gift>();
            }
            return _giftrepository.GetAllGifts();
        }
        public void AddGift(Models.Gift gift)
        {
            _giftrepository.AddGift(gift);
        }
        public void UpdateGift(Models.Gift gift)
        {
            _giftrepository.UpdateGift(gift);
        }
        public void DeleteGift(int id)
        {
            _giftrepository.DeleteGift(id);
        }  

        
    }
}

using Server.Models;
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

        public List<Models.GiftConnection> GetAllGifts()
        {
            var allGifts = _giftrepository.GetAllGifts();
            if (allGifts == null) 
            { 
                 return new List<Models.GiftConnection>();
            }
            return _giftrepository.GetAllGifts();
        }
        public void AddGift(Models.GiftConnection gift)
        {
            _giftrepository.AddGift(gift);
        }
        public void UpdateGift(Models.GiftConnection gift)
        {
            _giftrepository.UpdateGift(gift);
        }
        public void DeleteGift(int id)
        {
            _giftrepository.DeleteGift(id);
        }

        public GiftConnection GetGiftByName(string name)
        {
            return _giftrepository.GetGiftByName(name);
        }

        public List<GiftConnection> GetGiftsByDonorName(string donorName)
        {
           return _giftrepository.GetGiftsByDonorName(donorName);
        }

        public List<GiftConnection> OrderByPrice()
        {
           return  _giftrepository.OrderByPrice();
        }

        public List<GiftConnection> OrderByCategory(string category)
        {
            return _giftrepository.OrderByCategory(category);
        }
    }
}

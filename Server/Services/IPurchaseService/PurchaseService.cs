using Server.Models;
using Server.Repositories.Gift;
using Server.Repositories.Purchase;

namespace Server.Services.IPurchaseService
{
    public class PurchaseService:IPurchaseService
    {
        private readonly IPurchaseRepository _purchasesrepository;
        public PurchaseService(IPurchaseRepository purchasesrepository)
        {
            _purchasesrepository = purchasesrepository;
        }

        public List<PurchaseDetails> GetPurchaseByGift(string giftName)
        {
           return _purchasesrepository.GetPurchaseByGift(giftName);
        }

        public List<PurchaseDetails> GetPurchases()
        {
           return _purchasesrepository.GetPurchases();
        }

       

        public List<PurchaseDetails> OrderByExpensiveGift()
        {
            return _purchasesrepository.OrderByExpensiveGift();
        }
        public List<PurchaseDetails> OrderByAmountTicket()
        {
            return _purchasesrepository.OrderByAmountTicket();
        }
    }
}

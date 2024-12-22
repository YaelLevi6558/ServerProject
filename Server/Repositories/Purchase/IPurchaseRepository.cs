namespace Server.Repositories.Purchase
{
    public interface IPurchaseRepository
    {
        List<Models.PurchaseDetails> GetPurchaseByGift(string giftName);
        List<Models.PurchaseDetails> GetPurchases();
        List<Models.PurchaseDetails> OrderByExpensiveGift();
        List<Models.PurchaseDetails> OrderByAmountTicket();
        void GenerateSalesRevenueReport();


    }
}

namespace Server.Services.IPurchaseService
{
    public interface IPurchaseService
    {
        List<Models.PurchaseDetails> GetPurchaseByGift(string giftName);
        List<Models.PurchaseDetails> GetPurchases();
        List<Models.PurchaseDetails> OrderByExpensiveGift();
        List<Models.PurchaseDetails> OrderByAmountTicket();
    }
}

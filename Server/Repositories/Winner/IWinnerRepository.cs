namespace Server.Repositories.Winner
{
    public interface IWinnerRepository
    {
        void RandomToGift();
        void RandomToGift(int id);
        void GenerateExcelReport();
        //void SendWinnerEmail(string recipientEmail, string winnerName, string giftName);

    }
}

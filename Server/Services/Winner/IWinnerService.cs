namespace Server.Services.Winner
{
    public interface IWinnerService
    {
        void RandomToGift();
        void RandomToGift(int id);
        void GenerateExcelReport();


    }

}

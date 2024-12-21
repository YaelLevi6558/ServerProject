using Server.Repositories.Winner;

namespace Server.Services.Winner
{
    public class WinnerService:IWinnerService
    {
        private readonly IWinnerRepository _winnerrepository;
        public WinnerService(IWinnerRepository winnerrepository)
        {
            _winnerrepository = winnerrepository;
        }

        public void RandomToGift()
        {
           _winnerrepository.RandomToGift();
        }
    }
}

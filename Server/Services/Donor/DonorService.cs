using Server.Repositories.Donor;

namespace Server.Services.Donor
{
    public class DonorService:IDonorService
    {
        private readonly IDonorRepository _donorrepository;
        public DonorService(IDonorRepository donorrepository)
        {
            _donorrepository = donorrepository;
        }
        public List<Models.Donor> GetAllDonors()
        {
            var allDonors =  _donorrepository.GetAllDonors();
            if (allDonors == null)
            {
                return new List<Models.Donor>();
            }
            return allDonors;
        }

        public void AddDonor(Models.Donor donor)
        {
            _donorrepository.AddDonor(donor);
        }
        public void DeleteDonor(int id)
        {
            _donorrepository.DeleteDonor(id);
        }
        public void UpdateDonor(Models.Donor donor)
        {
            _donorrepository.UpdateDonor(donor);
        }

        public List<Models.Gift> GetDonorGifts(int id)
        {
            return _donorrepository.GetDonorGifts(id);
        }
    }
}

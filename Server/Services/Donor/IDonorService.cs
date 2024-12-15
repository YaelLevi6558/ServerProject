namespace Server.Services.Donor
{
    public interface IDonorService
    {
        List<Models.Donor> GetAllDonors();
        void AddDonor(Models.Donor donor);
        void UpdateDonor(Models.Donor donor);
        void DeleteDonor(int id);
        List<Models.Gift> GetDonorGifts(int id);

    }
}

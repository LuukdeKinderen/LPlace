using Service_Advertisement.DTO;
using Service_Advertisement.Models;

namespace Service_Advertisement.Database.Interfaces
{
    public interface IAdvertisementRepository
    {
        public int CreateAdvertisement(AdvertisementCreate advertisement, int userId);
        public IEnumerable<Advertisement> GetAdvertisements();
        public Advertisement GetAdvertisementById(int id);
        public void UpdateAdvertisement(AdvertisementUpdate advertisement);
        public void DeleteAdvertisement(int id);


        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int userId);
        public User GetUserById(int userId);
    }
}

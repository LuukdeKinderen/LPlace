using Service_Advertisement.Database.Interfaces;
using Service_Advertisement.DTO.Interfaces;
using Service_Advertisement.Models;

namespace Service_Advertisement.DTO
{
    public class AdvertisementResponseFactory : IAdvertisementResponseFactory
    {
        private readonly IAdvertisementRepository _advertisementRepository; 

        public AdvertisementResponseFactory(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public AdvertisementResponse GetAdvertisementResponse(Advertisement advertisement)
        {
            //get user by advertisement
            User user = _advertisementRepository.GetUserById(advertisement.UserId);

            return new AdvertisementResponse()
            {
                AdvertisementID = advertisement.AdvertisementID,
                AdvertisementAmount = advertisement.AdvertisementAmount,
                AdvertisementName = advertisement.AdvertisementName,
                ValidUntil = advertisement.ValidUntil,
                User = new UserDTO()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Location = new Location()
                    {
                        ZipCode = user.ZipCode,
                        HouseNumber = user.HouseNumber,
                        HouseNumberAddition = user.HouseNumberAddition,
                        City = user.City,
                        StreetName = user.StreetName,
                        Latitude = user.Latitude,
                        Longitude = user.Longitude,
                    }
                }
            };
        }
    }
}

using Service_Advertisement.Models;

namespace Service_Advertisement.DTO.Interfaces
{
    public interface IAdvertisementResponseFactory
    {
        public AdvertisementResponse GetAdvertisementResponse(Advertisement advertisement);
    }
}

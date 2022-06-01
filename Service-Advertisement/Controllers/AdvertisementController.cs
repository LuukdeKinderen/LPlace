using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Advertisement.Database;
using Service_Advertisement.Database.Interfaces;
using Service_Advertisement.DTO;
using Service_Advertisement.DTO.Interfaces;
using Service_Advertisement.Models;

namespace Service_Advertisement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementController : ControllerBase
    {

        private readonly ILogger<AdvertisementController> _logger;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IAdvertisementResponseFactory _advertisementResponseFactory;

        public AdvertisementController(
            ILogger<AdvertisementController> logger,
            IAdvertisementRepository advertisementRepository,
            IAdvertisementResponseFactory advertisementResponseFactory)
        {
            _logger = logger;
            _advertisementRepository = advertisementRepository;
            _advertisementResponseFactory = advertisementResponseFactory;
        }

        [HttpGet("{id}")]
        public AdvertisementResponse Read(int id)
        {
            Advertisement advertisement = _advertisementRepository.GetAdvertisementById(id);

            return _advertisementResponseFactory.GetAdvertisementResponse(advertisement);
        }

        [HttpGet]
        public IEnumerable<AdvertisementResponse> ReadAll()
        {

            return _advertisementRepository.GetAdvertisements().Select(a => _advertisementResponseFactory.GetAdvertisementResponse(a));
        }

        [HttpPost]
        public AdvertisementResponse Create(AdvertisementCreate request)
        {
            int id = _advertisementRepository.CreateAdvertisement(request);
            if (id > 0)
            {
                Advertisement advertisement = _advertisementRepository.GetAdvertisementById(id);
                return _advertisementResponseFactory.GetAdvertisementResponse(advertisement);
            }
            else return null;
        }

        [HttpPut]
        public AdvertisementResponse Update(AdvertisementUpdate request)
        {
            ////TODO GET FROM JWT
            //User u = _advertisementRepository.GetUserById(1);

            //_advertisementRepository.GetAdvertisementById(request.AdvertisementID);

            _advertisementRepository.UpdateAdvertisement(request);
            Advertisement advertisement = _advertisementRepository.GetAdvertisementById(request.AdvertisementID);
            return _advertisementResponseFactory.GetAdvertisementResponse(advertisement);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _advertisementRepository.DeleteAdvertisement(id);
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Advertisement.Database;
using Service_Advertisement.Database.Interfaces;
using Service_Advertisement.DTO;
using Service_Advertisement.Models;

namespace Service_Advertisement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly AdvertisementContext _context;

        private readonly ILogger<AdvertisementController> _logger;
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementController(
            ILogger<AdvertisementController> logger,
            AdvertisementContext context,
            IAdvertisementRepository advertisementRepository)
        {
            _logger = logger;
            _context = context;
            _advertisementRepository = advertisementRepository;
        }

        [HttpGet("{id}")]
        public AdvertisementResponse Read(int id)
        {
            return _advertisementRepository.GetAdvertisementById(id);
        }

        [HttpGet]
        public IEnumerable<AdvertisementResponse> ReadAll()
        {
            return _advertisementRepository.GetAdvertisements();
        }

        [HttpPost]
        public AdvertisementResponse Create(AdvertisementCreate request)
        {
            int id = _advertisementRepository.CreateAdvertisement(request);
            if (id > 0)
            {
                return _advertisementRepository.GetAdvertisementById(id);
            }
            else return null;
        }

        [HttpPut]
        public AdvertisementResponse Update(AdvertisementUpdate request)
        {
            _advertisementRepository.UpdateAdvertisement(request);
            return _advertisementRepository.GetAdvertisementById(request.AdvertisementID);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _advertisementRepository.DeleteAdvertisement(id);
        }

    }
}
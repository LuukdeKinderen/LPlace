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
            //ILogger<AdvertisementController> logger,
            IAdvertisementRepository advertisementRepository,
            IAdvertisementResponseFactory advertisementResponseFactory)
        {
            //_logger = logger;
            _advertisementRepository = advertisementRepository;
            _advertisementResponseFactory = advertisementResponseFactory;
        }

        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            Advertisement advertisement = _advertisementRepository.GetAdvertisementById(id);
            if (advertisement == null)
            {
                return new BadRequestObjectResult($"Advertisement with Id: {id} does not exist");
            }
            return new OkObjectResult(_advertisementResponseFactory.GetAdvertisementResponse(advertisement));            
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            return new OkObjectResult(_advertisementRepository.GetAdvertisements().Select(a => _advertisementResponseFactory.GetAdvertisementResponse(a)));
        }

        [HttpPost]
        public IActionResult Create(AdvertisementCreate request, int userId)
        {
            //TODO: Get userId from JWT
            int id = _advertisementRepository.CreateAdvertisement(request, userId);
            if (id > 0)
            {
                Advertisement advertisement = _advertisementRepository.GetAdvertisementById(id);
                return new OkObjectResult(_advertisementResponseFactory.GetAdvertisementResponse(advertisement));
            }
            else return new BadRequestObjectResult($"User: {userId} does not exist");
        }

        [HttpPut]
        public IActionResult Update(AdvertisementUpdate request, int userId)
        {
            //TODO: Get userId from JWT
            Advertisement advertisement = _advertisementRepository.GetAdvertisementById(request.AdvertisementID);
            if (advertisement == null)
            {
                return new BadRequestObjectResult($"Could not update Advertisement, advertisement with Id:'{request.AdvertisementID}' does not exist");
            }
            if (advertisement.UserId != userId)
            {
                return new BadRequestObjectResult($"User:'{userId}' is not authorised to update advertisement:'{request.AdvertisementID}'");
            }
            
            _advertisementRepository.UpdateAdvertisement(request);
            Advertisement updatedAdvertisement = _advertisementRepository.GetAdvertisementById(request.AdvertisementID);
            return new OkObjectResult(_advertisementResponseFactory.GetAdvertisementResponse(updatedAdvertisement));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, int userId)
        {
            //TODO: Get userId from JWT
            Advertisement advertisement = _advertisementRepository.GetAdvertisementById(id);
            if (advertisement == null)
            {
                return new BadRequestObjectResult($"Could not delete Advertisement, advertisement with Id:'{id}' does not exist");
            }
            if (advertisement.UserId != userId)
            {
                return new BadRequestObjectResult($"User:'{userId}' is not authorised to delete advertisement:'{id}'");
            }

            _advertisementRepository.DeleteAdvertisement(id);

            return new OkObjectResult($"Succesfully deleted advertisement: '{id}'");
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service_Advertisement.Controllers;
using Service_Advertisement.Database.Interfaces;
using Service_Advertisement.DTO;
using Service_Advertisement.DTO.Interfaces;
using Service_Advertisement.Models;
using Xunit;

namespace Service_Advertisement_Test
{
    public class ControllerTests
    {
        private readonly Mock<IAdvertisementResponseFactory> advertisementResponseFactory;
        public ControllerTests()
        {
            advertisementResponseFactory = new Mock<IAdvertisementResponseFactory>(MockBehavior.Strict);
            advertisementResponseFactory.Setup(s => s.GetAdvertisementResponse(It.IsAny<Advertisement>())).Returns(() => { return new AdvertisementResponse(); });
        }

        [Fact]
        public void Update_UserOwnsAdvertisement_OkObjectResult()
        {
            //Arange
            int requestUserId = 1;
            int responceUserId = 1;
            var advertisementRepository = new Mock<IAdvertisementRepository>(MockBehavior.Loose);
            AdvertisementUpdate advertisementUpdate = new AdvertisementUpdate();
            Advertisement advertisementResult = new Advertisement()
            {
                AdvertisementID = 1,
                UserId = responceUserId
            };

            advertisementRepository.Setup(s => s.GetAdvertisementById(It.IsAny<int>())).Returns(() => advertisementResult);

            AdvertisementController controller = new AdvertisementController(advertisementRepository.Object, advertisementResponseFactory.Object);

            //Act
            IActionResult result = controller.Update(advertisementUpdate, requestUserId);


            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_UserDoesNotOwnAdvertisement_BadRequestObjectResult()
        {
            //Arange
            int requestUserId = 2;
            int responceUserId = 1;
            var advertisementRepository = new Mock<IAdvertisementRepository>(MockBehavior.Loose);
            AdvertisementUpdate advertisementUpdate = new AdvertisementUpdate();
            Advertisement advertisementResult = new Advertisement()
            {
                AdvertisementID = 1,
                UserId = responceUserId
            };

            advertisementRepository.Setup(s => s.GetAdvertisementById(It.IsAny<int>())).Returns(() => advertisementResult);

            AdvertisementController controller = new AdvertisementController(advertisementRepository.Object, advertisementResponseFactory.Object);
            
            //Act
            IActionResult result = controller.Update(advertisementUpdate, requestUserId);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Update_AdvertisementDoesNotExist_BadRequestObjectResult()
        {
            //Arange
            int requestUserId = 1;
            var advertisementRepository = new Mock<IAdvertisementRepository>(MockBehavior.Loose);
            AdvertisementUpdate advertisementUpdate = new AdvertisementUpdate();
            Advertisement advertisementResult = null;

            advertisementRepository.Setup(s => s.GetAdvertisementById(It.IsAny<int>())).Returns(() => advertisementResult);

            AdvertisementController controller = new AdvertisementController(advertisementRepository.Object, advertisementResponseFactory.Object);

            //Act
            IActionResult result = controller.Update(advertisementUpdate, requestUserId);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
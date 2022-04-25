using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Advertisement.Database;
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

        public AdvertisementController(
            ILogger<AdvertisementController> logger,
            AdvertisementContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public AdvertisementResponse Read(int id)
        {
            return _context.Advertisements
                .Where(a => a.AdvertisementID == id)
                .Select(a => new AdvertisementResponse()
                {
                    AdvertisementID = a.AdvertisementID,
                    AdvertisementAmount = a.AdvertisementAmount,
                    AdvertisementName = a.AdvertisementName,

                    User = new DTO.User()
                    {
                        UserId = a.UserId,
                        FirstName = a.User.FirstName,
                        LastName = a.User.LastName,
                        Location = new Location()
                        {
                            ZipCode = a.User.ZipCode,
                            HouseNumber = a.User.HouseNumber,
                            HouseNumberAddition = a.User.HouseNumberAddition,
                            City = a.User.City,
                            StreetName = a.User.StreetName,
                            Latitude = a.User.Latitude,
                            Longitude = a.User.Longitude,
                        }
                    }
                })
                .Single();
        }

        [HttpGet]
        public IEnumerable<AdvertisementResponse> ReadAll()
        {
            return _context.Advertisements
                .Select(a => new AdvertisementResponse()
                {
                    AdvertisementID = a.AdvertisementID,
                    AdvertisementAmount = a.AdvertisementAmount,
                    AdvertisementName = a.AdvertisementName,

                    User = new DTO.User()
                    {
                        UserId = a.UserId,
                        FirstName = a.User.FirstName,
                        LastName = a.User.LastName,
                        Location = new Location()
                        {
                            ZipCode = a.User.ZipCode,
                            HouseNumber = a.User.HouseNumber,
                            HouseNumberAddition = a.User.HouseNumberAddition,
                            City = a.User.City,
                            StreetName = a.User.StreetName,
                            Latitude = a.User.Latitude,
                            Longitude = a.User.Longitude,
                        }
                    }
                })
                .ToList();
        }

        [HttpPost]
        public AdvertisementResponse Create(AdvertisementPost request)
        {
            //Get user from Database
            Models.User u = _context.Users.Single(u => u.UserId == request.UserId);


            //Create Advertisement object
            Advertisement a = new Advertisement()
            {
                AdvertisementName = request.AdvertisementName,
                AdvertisementAmount = request.AdvertisementAmount,
                User = u
            };

            //add advertisement to database
            _context.Advertisements.Add(a);
            _context.SaveChanges();

            //Get response from database
            AdvertisementResponse response = Read(a.AdvertisementID);
            return response;
        }



        [HttpPut]
        public AdvertisementResponse Update(AdvertisementPut request)
        {
            //Get Current advertisement
            Advertisement advertisement = _context.Advertisements
                .Single(a => a.AdvertisementID == request.AdvertisementID);

            //Update values
            advertisement.AdvertisementName = request.AdvertisementName;
            advertisement.AdvertisementAmount = request.AdvertisementAmount;
           
            //update database
            _context.Advertisements.Update(advertisement);
            _context.SaveChanges();

            //Get response from database
            AdvertisementResponse response = Read(request.AdvertisementID);
            return response;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Get advertisement by id
            Advertisement advertisement = _context.Advertisements
                .Single(a => a.AdvertisementID == id);


            //remove advertisement
            _context.Advertisements.Remove(advertisement);
            _context.SaveChanges();
        }

    }
}
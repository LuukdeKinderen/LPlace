using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Service_User.Database;
using Service_User.Models;
using RabbitMQ_Models.User;
using Service_User.DTO;

namespace Service_User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        private readonly ILogger<UserController> _logger;
        private readonly IBus _bus;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserController(
            ILogger<UserController> logger,
            UserContext context,
            IBus bus,
            IPublishEndpoint publishEndpoint
            )
        {
            _logger = logger;
            _context = context;
            _bus = bus;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{id}")]
        public UserResponse Read(int id)
        {
            UserResponse user = _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserResponse()
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,

                    Location = new Location()
                    {
                        ZipCode = u.ZipCode,
                        HouseNumber = u.HouseNumber,
                        HouseNumberAddition = u.HouseNumberAddition,
                        City = u.City,
                        StreetName = u.StreetName,
                        Longitude = u.Longitude,
                        Latitude = u.Latitude,
                    }
                })
                .Single();


            return user;
        }

        [HttpGet]
        public IEnumerable<UserResponse> ReadAll()
        {
            IEnumerable<UserResponse> users = _context.Users
                .Select(u => new UserResponse()
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,

                    Location = new Location()
                    {
                        ZipCode = u.ZipCode,
                        HouseNumber = u.HouseNumber,
                        HouseNumberAddition = u.HouseNumberAddition,
                        City = u.City,
                        StreetName = u.StreetName,
                        Longitude = u.Longitude,
                        Latitude = u.Latitude,
                    }
                })
                .ToList();
            return users;
        }

        [HttpPost]
        public async Task<UserResponse> Create(UserPost request)
        {
            //TODO: get from API
            double longitude = Random.Shared.NextDouble() * 100;
            double latitude = Random.Shared.NextDouble() * 100;

            //Create user
            Models.User user = new Models.User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,

                ZipCode = request.ZipCode,
                HouseNumber = request.HouseNumber,
                HouseNumberAddition = request.HouseNumberAddition,
                City = request.City,
                StreetName = request.StreetName,
                Longitude = longitude,
                Latitude = latitude,
            };

            //Save User in database
            _context.Users.Add(user);
            _context.SaveChanges();


            //Publish user to Messagebus
            UserCreated message = new UserCreated()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,

                ZipCode = user.ZipCode,
                HouseNumber = user.HouseNumber,
                HouseNumberAddition = user.HouseNumberAddition,
                StreetName = user.StreetName,
                City = user.City,

                Latitude = user.Latitude,
                Longitude = user.Longitude
            };
            await _publishEndpoint.Publish(message);

            //Convert to response
            UserResponse response = new UserResponse()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,

                Location = new Location()
                {
                    ZipCode = user.ZipCode,
                    HouseNumber = user.HouseNumber,
                    HouseNumberAddition = user.HouseNumberAddition,
                    StreetName = user.StreetName,
                    City = user.City,

                    Latitude = user.Latitude,
                    Longitude = user.Longitude
                }
            };
            return response;
        }

        [HttpPut]
        public async Task<UserResponse> Update(UserPut request)
        {
            //TODO: get from API
            double longitude = Random.Shared.NextDouble() * 100;
            double latitude = Random.Shared.NextDouble() * 100;

            //Get user
            Models.User user = _context.Users.Where(u => u.UserId == request.UserId).Single();

            //Update values
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            user.ZipCode = request.ZipCode;
            user.HouseNumber = request.HouseNumber;
            user.HouseNumberAddition = request.HouseNumberAddition;
            user.StreetName = request.StreetName;
            user.City = request.City;
            user.Latitude = latitude;
            user.Longitude = longitude;

            //Update Database
            _context.Users.Update(user);
            _context.SaveChanges();

            //Send to messagebus
            UserUpdated message = new UserUpdated()
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,

                ZipCode = request.ZipCode,
                HouseNumber = request.HouseNumber,
                HouseNumberAddition = request.HouseNumberAddition,
                StreetName = request.StreetName,
                City = request.City,

                Latitude = latitude,
                Longitude = longitude
            };
            await _publishEndpoint.Publish(message);

            //Convert to Response
            UserResponse response = new UserResponse()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Location = new Location()
                {
                    ZipCode = user.ZipCode,
                    HouseNumber = user.HouseNumber,
                    HouseNumberAddition = user.HouseNumberAddition,
                    StreetName = user.StreetName,
                    City = user.City,
                    Latitude = user.Latitude,
                    Longitude = user.Longitude
                }
            };
            return response;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Models.User user = _context.Users.Single(u => u.UserId == id);

            UserDeleted message = new UserDeleted()
            {
                UserId = user.UserId,
            };

            await _publishEndpoint.Publish(message);

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
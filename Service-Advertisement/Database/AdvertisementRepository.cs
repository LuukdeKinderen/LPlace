using Service_Advertisement.Database.Interfaces;
using Service_Advertisement.DTO;
using Service_Advertisement.Models;

namespace Service_Advertisement.Database
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IAdvertisementContext _context;
        private readonly ILogger<AdvertisementRepository> _logger;

        public AdvertisementRepository(IAdvertisementContext context, ILogger<AdvertisementRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int CreateAdvertisement(AdvertisementCreate advertisement)
        {
            //Get user from Database
            User? u = _context.Users.Where(u => u.UserId == advertisement.UserId).SingleOrDefault();

            if (u == null)
            {
                _logger.LogError($"Could not create advertisement. User {advertisement.UserId} does not exist");
                return -1;
            }

            //Create Advertisement object
            Advertisement a = new Advertisement()
            {
                AdvertisementName = advertisement.AdvertisementName,
                AdvertisementAmount = advertisement.AdvertisementAmount,
                User = u
            };

            //add advertisement to database
            _context.Advertisements.Add(a);
            _context.SaveChanges();

            //return id
            return a.AdvertisementID;
        }

        public AdvertisementResponse GetAdvertisementById(int id)
        {
            AdvertisementResponse? response =  _context.Advertisements.Where(a => a.AdvertisementID == id).Select(a => new AdvertisementResponse()
            {
                AdvertisementID = a.AdvertisementID,
                AdvertisementAmount = a.AdvertisementAmount,
                AdvertisementName = a.AdvertisementName,

                User = new DTO.UserDTO()
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
            }).Single();
            if (response == null)
            {
                _logger.LogError($"Advertisement {id} does not exist");
            }
            return response;
        }

        public IEnumerable<AdvertisementResponse> GetAdvertisements()
        {
            return _context.Advertisements.Select(a => new AdvertisementResponse()
            {
                AdvertisementID = a.AdvertisementID,
                AdvertisementAmount = a.AdvertisementAmount,
                AdvertisementName = a.AdvertisementName,

                User = new DTO.UserDTO()
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
            }).ToList();
        }

        public void UpdateAdvertisement(AdvertisementUpdate advertisement)
        {
            //Get Current advertisement
            Advertisement? a = _context.Advertisements.Where(a => a.AdvertisementID == advertisement.AdvertisementID).SingleOrDefault();

            if (a == null)
            {
                _logger.LogError($"Could not update advertisement. Advertisement {advertisement.AdvertisementID} does not exist");
                return;
            }

            //Update values
            a.AdvertisementName = advertisement.AdvertisementName;
            a.AdvertisementAmount = advertisement.AdvertisementAmount;

            //update database
            _context.Advertisements.Update(a);
            _context.SaveChanges();
        }

        public void DeleteAdvertisement(int id)
        {
            //Get advertisement
            Advertisement? a = _context.Advertisements.Where(a => a.AdvertisementID == id).SingleOrDefault();

            if(a == null)
            {
                _logger.LogError($"Could not delete advertisement. Advertisement {id} does not exist");
                return;
            }

            _context.Advertisements.Remove(a);
            _context.SaveChanges();
        }


        //User
        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

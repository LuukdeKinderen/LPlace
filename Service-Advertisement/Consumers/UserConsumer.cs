using MassTransit;
using RabbitMQ_Models;
using RabbitMQ_Models.User;
using Service_Advertisement;
using Service_Advertisement.Database;
using Service_Advertisement.Models;

namespace Service_Advertisement.Consumers
{
    public class UserConsumer : IConsumer<UserCreated>, IConsumer<UserUpdated> , IConsumer<UserDeleted>
    {
        private readonly AdvertisementContext _context;

        public UserConsumer(AdvertisementContext context)
        {
            _context = context;
        }

        public Task Consume(ConsumeContext<UserCreated> context)
        {
            Models.User user = new Models.User()
            {
                UserId = context.Message.UserId,
                FirstName = context.Message.FirstName,
                LastName = context.Message.LastName,
                
                ZipCode = context.Message.ZipCode,
                HouseNumber = context.Message.HouseNumber,
                HouseNumberAddition = context.Message.HouseNumberAddition,
                StreetName = context.Message.StreetName,
                City = context.Message.City,

                Latitude = context.Message.Latitude,
                Longitude = context.Message.Longitude,
            };
            
            _context.Users.Add(user);

            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<UserUpdated> context)
        {
            Models.User user = new Models.User()
            {
                UserId = context.Message.UserId,
                FirstName = context.Message.FirstName,
                LastName = context.Message.LastName,

                ZipCode = context.Message.ZipCode,
                HouseNumber = context.Message.HouseNumber,
                HouseNumberAddition = context.Message.HouseNumberAddition,
                StreetName = context.Message.StreetName,
                City = context.Message.City,

                Latitude = context.Message.Latitude,
                Longitude = context.Message.Longitude,
            };

            _context.Users.Update(user);

            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<UserDeleted> context)
        {
            Models.User user = _context.Users.Single(u => u.UserId == context.Message.UserId);

            //remove all linked advertisements
            _context.Advertisements.RemoveRange(_context.Advertisements.Where(a => a.UserId == context.Message.UserId));

            //remove user
            _context.Users.Remove(user);

            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}

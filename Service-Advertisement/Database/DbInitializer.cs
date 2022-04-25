using Service_Advertisement;
using Service_Advertisement.Models;

namespace Service_Advertisement.Database
{
    public static class DbInitializer
    {
        public static void Initialize(AdvertisementContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any() && context.Advertisements.Any())
            {
                return;
            }

            User user1 = new User()
            {
                UserId = 1, 
                FirstName = "Luuk",
                LastName = "de Kinderen",
                
                ZipCode = "5386 ja",
                HouseNumber = 1,
                HouseNumberAddition = null,
                StreetName = "D'n ham",
                City = "Geffen",
                Latitude = 51.7407979,
                Longitude = 5.452036
            };

            Advertisement advertisement1 = new Advertisement()
            {

                AdvertisementName = "A1",
                AdvertisementAmount = 100,
                User = user1
            };

            var advertisements = new Advertisement[]
            {
                advertisement1
            };
            foreach(var advertisement in advertisements)
            {
                context.Advertisements.Add(advertisement);
            }
            context.SaveChanges();

            var users = new User[]
            {
                user1
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}

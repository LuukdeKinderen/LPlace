using Service_User.Models;

namespace Service_User.Database
{
    public static class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{
                    FirstName ="Luuk", 
                    LastName ="de Kinderen", 
                    Email = "l.de.kinderen@student.fontys.nl",

                    ZipCode = "5386 ja",
                    HouseNumber = 1,
                    HouseNumberAddition = null,
                    StreetName = "D'n ham",
                    City = "Geffen",
                    Latitude = 51.7407979,
                    Longitude = 5.452036
                }
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}

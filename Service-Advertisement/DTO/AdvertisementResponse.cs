namespace Service_Advertisement.DTO
{
    public class AdvertisementResponse
    {
        public AdvertisementResponse()
        {
           
        }

        public int AdvertisementID { get; set; }
        public string AdvertisementName { get; set; }
        public double AdvertisementAmount { get; set; }
        public DateTime ValidUntil { get; set; }

        public UserDTO User { get; set; }
    }
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Location Location { get; set; }
    }
    public class Location
    {
        public string ZipCode { get; set; }
        public int HouseNumber { get; set; }
        public string? HouseNumberAddition { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

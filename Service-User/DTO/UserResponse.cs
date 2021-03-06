namespace Service_User.DTO
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


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

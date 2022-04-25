using System.ComponentModel.DataAnnotations;

namespace Service_User.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Email { get; set; }



        //Adress
        public string ZipCode { get; set; }
        public int HouseNumber { get; set; }
        public string? HouseNumberAddition { get; set; }

        
        public string StreetName { get; set; }
        public string City { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
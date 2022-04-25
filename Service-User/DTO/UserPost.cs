using System.ComponentModel.DataAnnotations;

namespace Service_User.DTO
{
    public class UserPost
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        //Adress
        [Required(ErrorMessage = "ZipCode is required")]
        [RegularExpression("^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$", ErrorMessage = "Invalid Zip code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "HouseNumber is required")]
        public int HouseNumber { get; set; }
        public string? HouseNumberAddition { get; set; }

        [Required(ErrorMessage = "StreetName is required")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
    }
}

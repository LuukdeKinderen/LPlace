using System.ComponentModel.DataAnnotations;

namespace Service_User.DTO
{
    public class UserPut
    {
        [Required(ErrorMessage = "UserID is required")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        //Adress
        [RegularExpression("^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$", ErrorMessage ="Invalid Zip code")]
        public string ZipCode { get; set; }
        public int HouseNumber { get; set; }
        public string HouseNumberAddition { get; set; }

        public string StreetName { get; set; }
        public string City { get; set; }
    }
}

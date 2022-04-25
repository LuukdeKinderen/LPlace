using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service_Advertisement.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        //Adress
        [Required]
        [RegularExpression("^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$")]
        public string ZipCode { get; set; }
        [Required]
        public int HouseNumber { get; set; }
        public string? HouseNumberAddition { get; set; }

        [Required]
        public string StreetName { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }
    }
}

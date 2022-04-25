using System.ComponentModel.DataAnnotations;

namespace Service_Advertisement.Models
{
    public class Advertisement
    {
        [Key]
        public int AdvertisementID { get; set; }

        [Required(ErrorMessage = "Advertisement name is required")]
        public string AdvertisementName { get; set; }

        [Required(ErrorMessage = "Advertisement amount is required")]
        public double AdvertisementAmount { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }
}
using Service_Advertisement.DTO;
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

        public DateTime ValidUntil { get; set; }

        public Advertisement()
        {
            ValidUntil = DateTime.Now + TimeSpan.FromDays(28);
        }

        public bool IsValid()
        {
            return DateTime.Now < ValidUntil;
        }
        public bool CanBeUpdatedBy(User user)
        {
            return user.UserId == UserId;
        }

        public void Renieuw()
        {
            ValidUntil = DateTime.Now + TimeSpan.FromDays(28);
        }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
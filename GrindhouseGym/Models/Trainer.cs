using System.ComponentModel.DataAnnotations;

namespace GrindhouseGym.Models
{
    public class Trainer
    {
        [Key]
        public int TrainerId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Specialization")]
        // e.g., "Yoga", "Weightlifting", "Cardio"
        public string Specialization { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Shift Time")]
        // e.g., "Morning (6AM - 2PM)"
        public string Shift { get; set; } = string.Empty;
        public ICollection<Member>? Members { get; set; }

        [Display(Name = "Profile Photo")]
        public string? ProfileImage { get; set; }
    }
}
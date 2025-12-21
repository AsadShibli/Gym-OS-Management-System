using System.ComponentModel.DataAnnotations;

namespace GrindhouseGym.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Stores the filename, e.g., "profile_123.jpg"
        public string? ProfileImage { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

        // === NEW FEATURE: Subscription Duration ===
        [Required]
        [Display(Name = "Duration (Months)")]
        [Range(1, 24, ErrorMessage = "Duration must be between 1 and 24 months")]
        public int Duration { get; set; } = 1; // Default to 1 month

        // Foreign Key
        public int PlanId { get; set; }

        // Navigation Property (Nullable to prevent validation errors on Create)
        public virtual Plan? Plan { get; set; }

        [Display(Name = "Assigned Trainer")]
        public int? TrainerId { get; set; } // Nullable, because not everyone has a trainer

        public Trainer? Trainer { get; set; } // Navigation Property
    }
}
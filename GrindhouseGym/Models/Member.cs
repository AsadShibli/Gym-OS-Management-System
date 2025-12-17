namespace GrindhouseGym.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        // Stores the filename, e.g., "profile_123.jpg"
        public string? ProfileImage { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;

        // Foreign Key
        public int PlanId { get; set; }

        // FIX: Add '?' here too.
        // When you create a member, you only send the ID, not the whole Plan object.
        // Without '?', validation fails saying "Plan is required".
        public virtual Plan? Plan { get; set; }
    }
}
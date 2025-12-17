using System.ComponentModel.DataAnnotations.Schema;

namespace GrindhouseGym.Models // Make sure namespace matches yours
{
    public class Plan
    {
        public int PlanId { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fee { get; set; }

        // FIX: Add '?' to allow this to be null during creation
        public virtual ICollection<Member>? Members { get; set; }
    }
}
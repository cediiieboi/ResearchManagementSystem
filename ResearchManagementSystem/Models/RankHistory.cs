using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchManagementSystem.Models
{
    public class RankHistory
    {

        [Key]
        [Display(Name = "Rank")]
        public string rankName { get; set; }

        [Display(Name = "College")]
        public string? College { get; set; }

        [Display(Name = "Duration")]
        public int? Year { get; set; }

        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(FacultyId))]
        [Display(Name = "FacultyName")]
        public string? UserId { get; set; }

        public ApplicationUser FacultyId { get; set; }

        [ForeignKey(nameof(FacultyEmail))]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public ApplicationUser FacultyEmail { get; set; }


    }
}
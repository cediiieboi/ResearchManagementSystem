using ResearchManagementSystem.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchManagementSystem.Models
{
    public class AddAccomplishment
    {
        [Key]
        public string ProductionId { get; set; } = Guid.NewGuid().ToString();
        public int? Year { get; set; }
        public string? TypeofResearch { get; set; }

        // Research Title will be auto-converted to uppercase
        private string? _researchTitle;
        [Display(Name = "Research Title")]
        public string? ResearchTitle
        {
            get { return _researchTitle; }
            set { _researchTitle = value?.ToUpper(); }
        }

        // Lead Researcher
        [ForeignKey(nameof(LeadResearcher))]
        [Display(Name = "Lead Researcher")]
        public string? LeadResearcherId { get; set; }
        public ApplicationUser? LeadResearcher { get; set; }

        // Co-Lead Researcher
        [ForeignKey(nameof(CoLeadResearcher))]
        [Display(Name = "Co-Lead Researcher")]
        public string? CoLeadResearcherId { get; set; }
        public ApplicationUser? CoLeadResearcher { get; set; }

        // Member Researchers
        [ForeignKey(nameof(Memberone))]
        [Display(Name = "Member")]
        public string? MemberoneId { get; set; }
        public ApplicationUser? Memberone { get; set; }

        [ForeignKey(nameof(Membertwo))]
        [Display(Name = "Member")]
        public string? MembertwoId { get; set; }
        public ApplicationUser? Membertwo { get; set; }

        [ForeignKey(nameof(Memberthree))]
        [Display(Name = "Member")]
        public string? MemberthreeId { get; set; }
        public ApplicationUser? Memberthree { get; set; }
        public string? Keyword1 { get; set; }
        public string? Keyword2 { get; set; }
        public string? Keyword3 { get; set; }
        public string? Keyword4 { get; set; }
        public string? Keyword5 { get; set; }
        public string? Keyword6 { get; set; }
        public string? Keyword7 { get; set; }
        public string? TypeofFunding { get; set; }
        // Other properties (College, Department, Funding, etc.)
        public string? College { get; set; }
        public string? Department { get; set; }
        public string? Branch_Campus { get; set; }

        [Display(Name = "Funding Agency")]
        public string? FundingAgency { get; set; }
        [Display(Name = "Amount of Funding")]
        public double? FundingAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Started")]
        public DateTime DateStarted { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Completion")]
        public DateTime? DateCompleted { get; set; }

        public byte[]? RequiredFile1Data { get; set; }
        public byte[]? RequiredFile2Data { get; set; }

        public byte[]? RequiredFile3Data { get; set; }
        public byte[]? ConditionalFileData { get; set; }

        public string? RequiredFile1Name { get; set; }
        public string? RequiredFile2Name { get; set; }

        public string? RequiredFile3Name { get; set; }
        public string? ConditionalFileName { get; set; }

        public bool IsStudentInvolved { get; set; }
        public string? Notes { get; set; }

        // User who created the accomplishment
        [ForeignKey(nameof(CreatedBy))]
        [Display(Name = "CreatedBy")]
        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }



        //// Collections for related accomplishments
        //public virtual ICollection<AddPresentation> Presentation { get; set; } = new List<AddPresentation>();
        //public virtual ICollection<AddPublication> Publication { get; set; } = new List<AddPublication>();
        //public virtual ICollection<AddUtilization> Utilization { get; set; } = new List<AddUtilization>();
        //public virtual ICollection<AddPatent> Patent { get; set; } = new List<AddPatent>();
        //public virtual ICollection<AddCitation> Citations { get; set; } = new List<AddCitation>();
        //public virtual ICollection<AddCitation> Copyright { get; set; } = new List<AddCopyright>();
    }

}




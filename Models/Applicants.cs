using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarrieJobsApp.Models
{
    public partial class Applicants
    {
        [Key]
        [Column("Applicant_Id")]
        public int ApplicantId { get; set; }
        [Required]
        [StringLength(50)]
        public string ApplicantResume { get; set; }
        [Required]
        [StringLength(50)]
        public string ApplicantLocation { get; set; }
        public int JobApplied { get; set; }

        [ForeignKey(nameof(JobApplied))]
        [InverseProperty(nameof(JobPostings.Applicants))]
        public virtual JobPostings JobAppliedNavigation { get; set; }
    }
}

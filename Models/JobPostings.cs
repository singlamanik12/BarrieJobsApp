using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarrieJobsApp.Models
{
    [Table("Job_Postings")]
    public partial class JobPostings
    {
        public JobPostings()
        {
            Applicants = new HashSet<Applicants>();
        }

        [Key]
        public int JobId { get; set; }
        [Required]
        [StringLength(50)]
        public string Company { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Column("Duration(months)")]
        public int? DurationMonths { get; set; }

        [InverseProperty("JobAppliedNavigation")]
        public virtual ICollection<Applicants> Applicants { get; set; }
    }
}

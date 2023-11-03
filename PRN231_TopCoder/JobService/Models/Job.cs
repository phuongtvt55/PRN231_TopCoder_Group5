using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace JobService.Models
{
    public partial class Job
    {
        public Job()
        {
            JobCategories = new HashSet<JobCategory>();
            JobRanks = new HashSet<JobRank>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int JobId { get; set; }
        public int? BusinessId { get; set; }
        public DateTime? PostDate { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string JobDetail { get; set; }
        //[Required(ErrorMessage = "Input can not be blank")]              
        public string Salary { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string JobRequirement { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string Skills { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string Website { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        [Range(1, 100, ErrorMessage = "YearExperience must be greater or equal than 0")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int? YearExperience { get; set; }
        [Required(ErrorMessage = "Input can not be blank")]
        public string ContractType { get; set; }
        public int? IsDelete { get; set; }
        public string Status { get; set; }

        public virtual ICollection<JobCategory> JobCategories { get; set; }
        public virtual ICollection<JobRank> JobRanks { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}

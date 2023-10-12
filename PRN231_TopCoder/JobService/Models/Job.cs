using System;
using System.Collections.Generic;

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
        public string JobTitle { get; set; }
        public string JobDetail { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string JobRequirement { get; set; }
        public string Skills { get; set; }
        public string Website { get; set; }
        public string Nationality { get; set; }
        public int? YearExperience { get; set; }
        public int? RankId { get; set; }
        public string ContractType { get; set; }
        public int? IsDelete { get; set; }
        public string Status { get; set; }

        public virtual ICollection<JobCategory> JobCategories { get; set; }
        public virtual ICollection<JobRank> JobRanks { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}

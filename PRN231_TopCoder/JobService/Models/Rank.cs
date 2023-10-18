using System;
using System.Collections.Generic;

#nullable disable

namespace JobService.Models
{
    public partial class Rank
    {
        public Rank()
        {
            JobRanks = new HashSet<JobRank>();
        }

        public int RankId { get; set; }
        public string RankName { get; set; }
        public int? IsDelete { get; set; }

        public virtual ICollection<JobRank> JobRanks { get; set; }
    }
}

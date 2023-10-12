using System;
using System.Collections.Generic;

#nullable disable

namespace JobService.Models
{
    public partial class JobRank
    {
        public int JobId { get; set; }
        public int RankId { get; set; }

        public virtual Job Job { get; set; }
        public virtual Rank Rank { get; set; }
    }
}

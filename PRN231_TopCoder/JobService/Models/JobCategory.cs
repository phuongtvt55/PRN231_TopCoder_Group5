using System;
using System.Collections.Generic;

#nullable disable

namespace JobService.Models
{
    public partial class JobCategory
    {
        public int JobId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Job Job { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace JobService.Models
{
    public partial class Category
    {
        public Category()
        {
            JobCategories = new HashSet<JobCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? IsDelete { get; set; }

        public virtual ICollection<JobCategory> JobCategories { get; set; }
    }
}

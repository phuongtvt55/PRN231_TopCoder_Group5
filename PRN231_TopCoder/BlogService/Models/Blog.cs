using System;
using System.Collections.Generic;

#nullable disable

namespace BlogService.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public int BlogId { get; set; }
        public int? UserId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDetail { get; set; }
        public string Image { get; set; }
        public int? IsDelete { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

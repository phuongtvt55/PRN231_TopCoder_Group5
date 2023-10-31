using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BlogService.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? BlogId { get; set; }
        public int? UserId { get; set; }
        [DisplayName("Comment Message")]
        public string CommentMsg { get; set; }
        [DisplayName("Comment Date")]
        public DateTime? CommentDate { get; set; }
        public int? Rate { get; set; }

        public virtual Blog Blog { get; set; }
    }
}

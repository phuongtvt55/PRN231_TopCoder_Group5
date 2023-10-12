using System;
using System.Collections.Generic;

#nullable disable

namespace JobService.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }
        public int? IsDelete { get; set; }

        public virtual Job Job { get; set; }
    }
}

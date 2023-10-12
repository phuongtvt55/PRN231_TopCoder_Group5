using System;
using System.Collections.Generic;

#nullable disable

namespace UserService.Models
{
    public partial class BusinessProfile
    {
        public int BusinessId { get; set; }
        public int? UserId { get; set; }
        public string CompanyName { get; set; }
        public string AboutCompany { get; set; }
        public int? IsDelete { get; set; }

        public virtual User User { get; set; }
    }
}

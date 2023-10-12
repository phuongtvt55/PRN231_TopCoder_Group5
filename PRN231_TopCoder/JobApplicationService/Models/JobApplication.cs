using System;
using System.Collections.Generic;

#nullable disable

namespace JobApplicationService.Models
{
    public partial class JobApplication
    {
        public int ApplicationId { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }
        public DateTime? ApplyDate { get; set; }
        public int? IsDelete { get; set; }
        public string Status { get; set; }
    }
}

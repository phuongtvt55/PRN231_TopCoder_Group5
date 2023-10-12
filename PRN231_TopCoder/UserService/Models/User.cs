using System;
using System.Collections.Generic;

#nullable disable

namespace UserService.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ImageProfile { get; set; }
        public string Cvprofile { get; set; }
        public string UserType { get; set; }
        public int? IsDelete { get; set; }

        public virtual BusinessProfile BusinessProfile { get; set; }
    }
}

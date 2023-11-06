using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace UserService.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        public string ImageProfile { get; set; }
        public string Cvprofile { get; set; }
        public string UserType { get; set; }
        public int? IsDelete { get; set; }

        public virtual BusinessProfile BusinessProfile { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPi.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; } = null!;

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = null!;

        public int PhoneNumber { get; set; } 

        [Column(TypeName ="nvarchar(max)")]
        public string PasswordHash { get; set; } = null!;

        [Column(TypeName = "nvarchar(max)")]
        public string PasswordSalt { get; set; } = null!;

        [Column(TypeName = "nvarchar(20)")]
        public string Gender { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(20)")]
        public string Role { get; set; } = "User";
    }
}

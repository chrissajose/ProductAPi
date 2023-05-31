using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPi.Dtos
{
    public class CustomerDtos
    {
        [Required]
        
        public string UserName { get; set; } = null!;

        [Required]
        
        public string FirstName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; } = null!;

        
        [Required]
        public string Gender { get; set; } = null!;
    }
}

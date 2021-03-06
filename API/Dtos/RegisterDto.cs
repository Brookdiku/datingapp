using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public String userName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public String password { get; set; }
    }
}
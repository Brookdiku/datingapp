using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class LoginDto
    {
        [Required]
        public String userName { get; set; }
        [Required]
        public String password { get; set; }
    }
}
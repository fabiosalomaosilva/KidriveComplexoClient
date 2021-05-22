using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KidriveComplexoClient.Models
{
    public class LoginModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

    }
}

using System;
using System.Linq;

namespace KidriveComplexoClient.Models
{

    public class LoginResult
    {
        public string nomeCompleto { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public string cargo { get; set; }
        public string setor { get; set; }
        public string setorId { get; set; }        
        public bool successful { get; set; }
        public string error { get; set; }
        public string token { get; set; }
    }
}

using System;

namespace KidriveComplexoClient.Models
{
    public class Setor
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string criadoPor { get; set; }
        public DateTime criadoEm { get; set; }
        public string alteradoPor { get; set; }
        public DateTime alteradoEm { get; set; }
        public bool ativo { get; set; }
    }

}

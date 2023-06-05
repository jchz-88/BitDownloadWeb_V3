using System.ComponentModel.DataAnnotations.Schema;

namespace BitDownloadWeb_V3.Model
{
    public class Usuarios
    {
        public int Id { get; set; }

        public int? Email { get; set; }

        public string? Clave { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }
        
    }
}

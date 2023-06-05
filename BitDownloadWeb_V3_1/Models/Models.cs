using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BitDownloadWeb_V3_1.Models
{


    /*
    public class Usuarios
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Clave { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }
        
    }
    */

    public class User
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }
        public int Activo { get; set; }

    }
        public class Usuarios
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Complete este campo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Complete este campo")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Complete este campo")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Complete este campo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Clave { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime UltimaConexion { get; set; }

        public int Activo { get; set; }

        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public TipoUsuario TipoUsuario { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }
    }

    public class TipoUsuario
    {
        public int Id { get; set; }
        public String Rol { get; set; }
    }

    public enum Status
    {
        Pending, Active, Canceled, Finished, Error, Retry
    }
    public class Download
    {
        /* Atributos Generales */
        public int ID { get; set; }
        [Required(ErrorMessage = "Complete este campo")]
        public int Order { get; set; }
        public string HostName { get; set; }

        public string Tag { get; set; }

        public int FileID { get; set; }
        [Required(ErrorMessage = "Complete este campo")]
        public string MoveTo { get; set; }
        [Required(ErrorMessage = "Complete este campo")]
        public int RetryCount { get; set; }

        /* Solicita ejecutar el archivo */
        public bool Execute { get; set; }
        public bool Delete { get; set; }
        public string Parameters { get; set; }

        /* Estado de la descarga */
        public long BytesTransfer { get; set; }
        public string TempPath { get; set; }
        public Status Status { get; set; }

        /* Estado de la Ejecución */
        public int ExitStatus { get; set; } = -1;
        public string Output { get; set; }
        public string LastUpdate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ML
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        [Required]
        public string Nombre { get; set; }
       
        [Required]
        [DisplayName ("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Required]
        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [DisplayName("Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        [StringLength(10)]
        public string Celular { get; set; }

        [Required]
        [StringLength(18)]
        public string CURP { get; set; }


        public string Imagen { get; set; }

        public ML.Rol Rol { get; set; } //propiedad de navegacion 
        public ML.Direccion Direccion { get; set; } //propiedad de navegacion 

        public List<object> Usuarios { get; set; }
    }
}

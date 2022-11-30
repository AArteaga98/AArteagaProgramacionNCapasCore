using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? NumeroEmpleado { get; set; }
        public string? Rfc { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Nss { get; set; }
        public string? FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public int IdEmpresa { get; set; }


        //PROP NAV

        public ML.Empresa? Empresa { get; set; }

    }
}

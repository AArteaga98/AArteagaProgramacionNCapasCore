using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public string? Genero { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Telefono { get; set; }

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public byte? IdRol { get; set; }

    public string? Imagen { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Aseguradora> Aseguradoras { get; } = new List<Aseguradora>();

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual Rol? IdRolNavigation { get; set; }

    //Agregadas
    public string? RolNombre { get; set; }
    public int IdDirreccion { get; set; }
    public string? Calle { get; set; }
    public string? NumeroInterior { get; set; }
    public string? NumeroExterior { get; set; }

    //COLONIA

    public int IdColonia { get; set; }
    public string? ColoniaNombre { get; set; }
    public string? CP { get; set; }

    //Municipio

    public int IdMunicipio { get; set; }
    public string? MunicipioNombre { get; set; }

    //Estado

    public int IdEstado { get; set; }
    public string? EstadoNombre { get; set; }

    //Pais

    public int IdPais { get; set; }
    public string? PaisNombre { get; set; }



}

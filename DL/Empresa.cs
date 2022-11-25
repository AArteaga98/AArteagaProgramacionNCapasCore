using System;
using System.Collections.Generic;

namespace DL;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string DireccionWeb { get; set; } = null!;

    public string? Logo { get; set; }
}

using System;
using System.Collections.Generic;

namespace DL;

public partial class Direccion
{
    public int IdDirreccion { get; set; }

    public string Calle { get; set; } = null!;

    public string NumeroInterior { get; set; } = null!;

    public string NumeroExterior { get; set; } = null!;

    public int? IdColonia { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Colonium? IdColoniaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}

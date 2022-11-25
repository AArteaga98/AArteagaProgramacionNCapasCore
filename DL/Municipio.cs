using System;
using System.Collections.Generic;

namespace DL;

public partial class Municipio
{
    public int IdMunicipio { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdEstado { get; set; }

    public virtual ICollection<Colonium> Colonia { get; } = new List<Colonium>();

    public virtual Estado? IdEstadoNavigation { get; set; }
}

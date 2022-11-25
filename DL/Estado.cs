using System;
using System.Collections.Generic;

namespace DL;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdPais { get; set; }

    public virtual Pai? IdPaisNavigation { get; set; }

    public virtual ICollection<Municipio> Municipios { get; } = new List<Municipio>();
}

using System;
using System.Collections.Generic;

namespace DL;

public partial class Pai
{
    public int IdPais { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Estado> Estados { get; } = new List<Estado>();
}

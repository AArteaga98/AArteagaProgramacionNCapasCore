using System;
using System.Collections.Generic;

namespace DL;

public partial class Rol
{
    public byte IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}

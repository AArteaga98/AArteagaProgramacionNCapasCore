﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        public int IdColonia { get; set; }
        public string? Nombre { get; set; }
        public string? CP { get; set; }


        public List<object>? Colonias { get; set; }
        public ML.Municipio? Municipio { get; set; }
    }
}

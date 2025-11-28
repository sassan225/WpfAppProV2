using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppProV2
{

  
        public class Proveedor
        {
            private static int contadorId = 1;

            public int IdProveedor { get; set; }
            public string NombreProveedor { get; set; }
            public string ContactoProveedor { get; set; }
            public string Ciudad { get; set; }

            public Proveedor()
            {
                IdProveedor = contadorId++;
            }

            public override string ToString()
            {
                return $"{IdProveedor},{NombreProveedor},{ContactoProveedor},{Ciudad}";
            }
        }
    }





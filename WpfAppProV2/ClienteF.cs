using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppProV2
{
    internal class ClienteF
    {
        private static int contadorId = 1;

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public ClienteF()
        {
            IdCliente = contadorId++;
        }

        public override string ToString()
        {
            return $"{IdCliente},{Nombre},{Correo}";
        }
    }
}

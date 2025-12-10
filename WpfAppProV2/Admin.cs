using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppProV2
{
    internal class Admin
    {
        private static int contadorId = 1;

        public int IdAdmin { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public Admin()
        {
            IdAdmin = contadorId++;
        }

        public override string ToString()
        {
            return $"{IdAdmin},{Nombre},{Correo},{Contraseña}";
        }
    }
}

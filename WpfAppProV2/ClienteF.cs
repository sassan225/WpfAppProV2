using System;

namespace WpfAppProV2
{
    public class ClienteF
    {
        // Propiedades
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        // Constructor vacío
        public ClienteF()
        {
        }

        // Constructor con parámetros
        public ClienteF(int id, string nombre, string correo, string telefono)
        {
            IdCliente = id;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
        }

       
        public override string ToString()
        {
            return $"{IdCliente},{Nombre},{Correo},{Telefono}";
        }
    }
}

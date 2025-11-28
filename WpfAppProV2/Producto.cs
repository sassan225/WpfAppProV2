using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfAppProV2
{
    public class Producto
    {

    public int Id{ get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public Producto() { }

    public Producto(int id, string nombre, string categoria, decimal precio, int stock)
    {
        Id = id;
        Nombre = nombre;
        Categoria = categoria;
        Precio = precio;
        Stock = stock;
    }
}
    }



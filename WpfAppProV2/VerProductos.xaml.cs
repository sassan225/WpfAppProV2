using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para VerProductos.xaml
    /// </summary>
    public partial class VerProductos : Window
    {
        private string rutaArchivo = "productos.txt";
        public VerProductos()
        {
            InitializeComponent();
            CargarProductos(); // Conexión: cargar productos al iniciar la ventana
        }

        private void CargarProductos()
        {
            List<Producto> lista = new List<Producto>();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split('|');

                    if (datos.Length == 5)
                    {
                        lista.Add(new Producto
                        {
                            IdProducto = int.Parse(datos[0]),
                            Nombre = datos[1],
                            Categoria = datos[2],
                            Precio = decimal.Parse(datos[3]),
                            Stock = int.Parse(datos[4])
                        });
                    }

                }
            }

            dgProductos.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Welcome w = new Welcome();
            w.Show();
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }
}


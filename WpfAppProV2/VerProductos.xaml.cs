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
        private readonly string rutaArchivo = @"C:\cosmetiqueSoftware\productos.txt";
        private readonly string _rolUsuario; // Nuevo campo para el rol

        // Modifica el constructor para aceptar el rol
        public VerProductos(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;
            _rolUsuario = rolUsuario;
            CargarProductos();
        }

        private void CargarProductos()
        {
            var lista = new List<Producto>();

            if (!File.Exists(rutaArchivo))
            {
                dgProductos.ItemsSource = lista;
                return;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length == 5)
                    {
                        if (int.TryParse(datos[0], out int id))
                        {
                            if (decimal.TryParse(datos[3], out decimal precio))
                            {
                                if (int.TryParse(datos[4], out int stock))
                                {
                                    lista.Add(new Producto(id, datos[1], datos[2], precio, stock));
                                }
                            }
                        }
                    }
                }
            }

            dgProductos.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            // Suponiendo que la ventana principal es MainWindow y NO acepta el rol como parámetro
            var mainWindow = new MainWindow();
            mainWindow.Show();
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


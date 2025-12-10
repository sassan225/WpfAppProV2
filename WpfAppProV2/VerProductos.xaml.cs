using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WpfAppProV2
{
    public partial class VerProductos : Window
    {
        private readonly string rutaArchivo = @"C:\cosmetiqueSoftware\productos.txt";
        private readonly string _rolUsuario;

        public VerProductos(string rolUsuario)
        {
            InitializeComponent();
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

                    if (datos.Length == 5 &&
                        int.TryParse(datos[0], out int id) &&
                        decimal.TryParse(datos[3], out decimal precio) &&
                        int.TryParse(datos[4], out int stock))
                    {
                        lista.Add(new Producto(id, datos[1], datos[2], precio, stock));
                    }
                }
            }

            dgProductos.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin();
            }
            else if (_rolUsuario.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new AdminPanel();
            }
            else
            {
                MessageBox.Show("Origen desconocido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ventanaDestino.Show();
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

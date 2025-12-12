using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

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
            List<Producto> lista = new List<Producto>();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        string[] datos = linea.Split(',');

                        if (datos.Length == 5)
                        {
                            int id;
                            decimal precio;
                            int stock;

                            if (int.TryParse(datos[0], out id) &&
                                decimal.TryParse(datos[3], out precio) &&
                                int.TryParse(datos[4], out stock))
                            {
                                Producto p = new Producto(id, datos[1], datos[2], precio, stock);
                                lista.Add(p);
                            }
                        }
                    }
                }
            }

            dgProductos.ItemsSource = null;
            dgProductos.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            }
            else if (_rolUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new AdminPanel(_rolUsuario);
            }
            else
            {
                MessageBox.Show("Rol desconocido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ventanaDestino.Show();
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegistrarProducto registrarProducto = new RegistrarProducto(_rolUsuario);
            registrarProducto.Show();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto productoSeleccionado)
            {
                // Leer todas las líneas del archivo
                var lineas = File.ReadAllLines(rutaArchivo);
                List<string> nuevasLineas = new List<string>();

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] datos = linea.Split(',');

                    // Mantenemos todas las líneas excepto la seleccionada
                    if (datos.Length == 5 && datos[0] != productoSeleccionado.Id.ToString())
                    {
                        nuevasLineas.Add(linea);
                    }
                }

                File.WriteAllLines(rutaArchivo, nuevasLineas);
                MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarProductos(); // Recargamos la tabla
            }
            else
            {
                MessageBox.Show("Selecciona un producto para borrar.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

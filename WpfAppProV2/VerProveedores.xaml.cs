using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class VerProveedores : Window
    {
        private readonly string carpetaBase = @"C:\cosmetiqueSoftware";
        private readonly string rutaArchivo;

        private readonly string _rolUsuario;   // MISMO ENFOQUE QUE EN RegistrarProducto Y VerProductos

        public VerProveedores(string rolUsuario)
        {
            InitializeComponent();

            _rolUsuario = rolUsuario;

            if (!Directory.Exists(carpetaBase))
                Directory.CreateDirectory(carpetaBase);

            rutaArchivo = Path.Combine(carpetaBase, "proveedores.txt");

            CargarProveedores();
        }

        private void CargarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length == 4)
                    {
                        lista.Add(new Proveedor
                        {
                            IdProveedor = int.Parse(datos[0]),
                            NombreProveedor = datos[1],
                            ContactoProveedor = datos[2],
                            Ciudad = datos[3]
                        });
                    }
                }
            }

            dgProveedores.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            }
            else if (_rolUsuario.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new AdminPanel(_rolUsuario);
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

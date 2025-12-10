using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class RegistrarProveedor : Window
    {
        private readonly string carpetaBase = @"C:\cosmetiqueSoftware";
        private readonly string rutaArchivo;
        private readonly string _rolUsuario; // Guardamos el rol

        // Constructor que recibe el rol
        public RegistrarProveedor(string rolUsuario)
        {
            InitializeComponent();

            _rolUsuario = rolUsuario;

            if (!Directory.Exists(carpetaBase))
                Directory.CreateDirectory(carpetaBase);

            rutaArchivo = Path.Combine(carpetaBase, "proveedores.txt");
        }

        private void btnGuardarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtContactoProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtCiudadProveedor.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Proveedor nuevo = new Proveedor
            {
                NombreProveedor = txtNombreProveedor.Text,
                ContactoProveedor = txtContactoProveedor.Text,
                Ciudad = txtCiudadProveedor.Text
            };

            File.AppendAllText(rutaArchivo, nuevo.ToString() + Environment.NewLine);

            MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            txtNombreProveedor.Clear();
            txtContactoProveedor.Clear();
            txtCiudadProveedor.Clear();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino = null;

            if (_rolUsuario.Equals("SUPERADMIN", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin();
            }
            else if (_rolUsuario.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new AdminPanel();
            }
            else
            {
                MessageBox.Show("No se pudo determinar el rol del usuario. Inicie sesión nuevamente.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            ventanaDestino.Show();
            this.Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class RegistrarProveedor : Window
    {
        private readonly string _rolUsuario;
        private readonly string _carpetaBase = @"C:\cosmetiqueSoftware";
        private readonly string _rutaArchivo;

        public RegistrarProveedor(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            // Crear carpeta base si no existe
            if (!Directory.Exists(_carpetaBase))
                Directory.CreateDirectory(_carpetaBase);

            _rutaArchivo = Path.Combine(_carpetaBase, "proveedores.txt");

            // Crear archivo si no existe
            if (!File.Exists(_rutaArchivo))
                File.WriteAllText(_rutaArchivo, string.Empty);
        }

        // Guardar proveedor
        private void btnGuardarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtContactoProveedor.Text) ||
                string.IsNullOrWhiteSpace(txtCiudadProveedor.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string linea = txtNombreProveedor.Text.Trim() + "," +
                               txtContactoProveedor.Text.Trim() + "," +
                               txtCiudadProveedor.Text.Trim();

                File.AppendAllText(_rutaArchivo, linea + Environment.NewLine);
                MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar proveedor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Limpiar campos
        private void LimpiarCampos()
        {
            txtNombreProveedor.Clear();
            txtContactoProveedor.Clear();
            txtCiudadProveedor.Clear();
        }

        // Volver a panel según rol
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino = null;
            string rol = _rolUsuario.ToLower();

            if (rol == "superadmin")
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            else if (rol == "admin")
                ventanaDestino = new AdminPanel(_rolUsuario);
            else
            {
                MessageBox.Show("Rol desconocido. Inicia sesión nuevamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            ventanaDestino.Show();
            this.Close();
        }

        // Cerrar ventana
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Minimizar ventana
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Mover ventana arrastrando
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}

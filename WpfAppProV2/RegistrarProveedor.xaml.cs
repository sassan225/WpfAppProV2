using System;
using System.IO;
using System.Windows;

namespace WpfAppProV2
{
    public partial class RegistrarProveedor : Window
    {
        private string carpetaBase = @"C:\cosmetiqueSoftware";
        private string rutaArchivo;

        public RegistrarProveedor()
        {
            InitializeComponent();

            // Crear carpeta si no existe
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

            // Guardar en archivo en la carpeta central
            File.AppendAllText(rutaArchivo, nuevo.ToString() + Environment.NewLine);

            MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            txtNombreProveedor.Clear();
            txtContactoProveedor.Clear();
            txtCiudadProveedor.Clear();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Welcome welcome = new Welcome();
            welcome.Show();
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

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }
}

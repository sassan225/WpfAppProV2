using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class RegistrarClienteF : Window
    {
        private readonly string _rutaArchivo;
        private readonly string _rolUsuario;
        private readonly string _carpetaBase = @"C:\cosmetiqueSoftware";

        public RegistrarClienteF(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            if (!Directory.Exists(_carpetaBase))
                Directory.CreateDirectory(_carpetaBase);

            _rutaArchivo = Path.Combine(_carpetaBase, "clientesF.txt");
            if (!File.Exists(_rutaArchivo))
                File.WriteAllText(_rutaArchivo, string.Empty);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;
            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            else
                ventanaDestino = new AdminPanel(_rolUsuario);

            ventanaDestino.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = File.ReadAllLines(_rutaArchivo).Length + 1;
            string linea = $"{id},{txtNombre.Text},{txtCorreo.Text},{txtTelefono.Text}";
            File.AppendAllText(_rutaArchivo, linea + Environment.NewLine);

            MessageBox.Show("Cliente frecuente registrado correctamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
        }
    }
}

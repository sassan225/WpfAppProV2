using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    /// <summary>
    /// Lógica de interacción para RegistrarAdmin.xaml
    /// </summary>
    public partial class RegistrarAdmin : Window
    {
        private readonly string rutaCarpeta = @"C:\cosmetiqueSoftware";
        private readonly string rutaArchivo;

        public RegistrarAdmin()
        {
            InitializeComponent();

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            rutaArchivo = Path.Combine(rutaCarpeta, "admins.txt");

            if (!File.Exists(rutaArchivo))
                File.WriteAllText(rutaArchivo, string.Empty);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtContrasena.Password))
                {
                    MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = File.ReadAllLines(rutaArchivo).Length + 1;
                string linea = $"{id},{txtNombre.Text},{txtCorreo.Text},{txtContrasena.Password}";
                File.AppendAllText(rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show("Administrador guardado correctamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtContrasena.Clear();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            PanelSuperAdmin panelSuperAdmin = new PanelSuperAdmin();
            panelSuperAdmin.Show();
            this.Close();
        }
    }
}

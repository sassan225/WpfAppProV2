using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class RegistrarClienteF : Window
    {
        private readonly string rutaCarpeta = @"C:\cosmetiqueSoftware";
        private readonly string rutaArchivo;

        private readonly string origen;   // MISMA LÓGICA QUE LOS OTROS FORMULARIOS

        public RegistrarClienteF(string origen = "admin")
        {
            InitializeComponent();

            this.origen = origen;

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            rutaArchivo = Path.Combine(rutaCarpeta, "clientesF.txt");

            if (!File.Exists(rutaArchivo))
                File.WriteAllText(rutaArchivo, string.Empty);
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (origen.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin();
            }
            else if (origen.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new AdminPanel();
            }
            else
            {
                MessageBox.Show(
                    "Origen desconocido.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            ventanaDestino.Show();
            this.Close();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show(
                        "Completa todos los campos.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                    return;
                }

                int id = File.ReadAllLines(rutaArchivo).Length + 1;

                string linea = $"{id},{txtNombre.Text},{txtCorreo.Text},{txtTelefono.Text}";
                File.AppendAllText(rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show(
                    "Cliente frecuente guardado correctamente!",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
        }
    }
}

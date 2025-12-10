using System;
using System.IO;
using System.Windows;

namespace WpfAppProV2
{
    public partial class RegistrarProducto : Window
    {
        private readonly string rutaCarpeta = @"C:\cosmetiqueSoftware";
        private readonly string rutaArchivo;
        private readonly string origen;

        public RegistrarProducto(string origen)
        {
            InitializeComponent();

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            rutaArchivo = Path.Combine(rutaCarpeta, "productos.txt");

            if (!File.Exists(rutaArchivo))  
                File.WriteAllText(rutaArchivo, string.Empty);

            this.origen = origen;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = File.ReadAllLines(rutaArchivo).Length + 1;

                Producto p = new Producto(id, txtNombre.Text, txtCategoria.Text,
                                          Convert.ToDecimal(txtPrecio.Text),
                                          Convert.ToInt32(txtStock.Text));

                string linea = $"{p.Id},{p.Nombre},{p.Categoria},{p.Precio},{p.Stock}";
                File.AppendAllText(rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show("Producto guardado correctamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
            txtCategoria.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.Close();
        private void BtnMinimize_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;
            if (origen.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin();
            }
            else if (origen.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
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
    }
}
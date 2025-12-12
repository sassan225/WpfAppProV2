using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class RegistrarProducto : Window
    {
        private readonly string _rutaArchivo;
        private readonly string _rolUsuario;
        private readonly string _carpetaBase = @"C:\cosmetiqueSoftware";

        public RegistrarProducto(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            if (!Directory.Exists(_carpetaBase))
            {
                Directory.CreateDirectory(_carpetaBase);
            }

            _rutaArchivo = Path.Combine(_carpetaBase, "productos.txt");

            if (!File.Exists(_rutaArchivo))
            {
                File.WriteAllText(_rutaArchivo, string.Empty);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    cmbCategoria.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int id = File.ReadAllLines(_rutaArchivo).Length + 1;

                string categoria = (cmbCategoria.SelectedItem as System.Windows.Controls.ComboBoxItem).Content.ToString();

                Producto p = new Producto(id, txtNombre.Text, categoria,
                                          Convert.ToDecimal(txtPrecio.Text),
                                          Convert.ToInt32(txtStock.Text));

                string linea = p.Id + "," + p.Nombre + "," + p.Categoria + "," + p.Precio + "," + p.Stock;
                File.AppendAllText(_rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show("Producto guardado correctamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtPrecio.Clear();
            txtStock.Clear();
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
            {
                this.DragMove();
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}

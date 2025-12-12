using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

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
                Directory.CreateDirectory(_carpetaBase);

            _rutaArchivo = Path.Combine(_carpetaBase, "productos.txt");
            if (!File.Exists(_rutaArchivo))
                File.WriteAllText(_rutaArchivo, string.Empty);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string categoria = (cmbCategoria.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            string precioText = txtPrecio.Text.Trim();
            string stockText = txtStock.Text.Trim();

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(categoria) ||
                string.IsNullOrWhiteSpace(precioText) || string.IsNullOrWhiteSpace(stockText))
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar nombre (solo letras, números y espacios)
            Regex nombreRegex = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\s]+$");
            if (!nombreRegex.IsMatch(nombre))
            {
                MessageBox.Show("El nombre del producto solo puede contener letras, números y espacios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar precio (decimal positivo)
            decimal precio;
            if (!Decimal.TryParse(precioText, out precio) || precio < 0)
            {
                MessageBox.Show("Ingresa un precio válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar stock (entero positivo)
            int stock;
            if (!Int32.TryParse(stockText, out stock) || stock < 0)
            {
                MessageBox.Show("Ingresa un stock válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int id = File.ReadAllLines(_rutaArchivo).Length + 1;
                string linea = id + "," + nombre + "," + categoria + "," + precio + "," + stock;
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
                this.DragMove();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            else if (_rolUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase))
                ventanaDestino = new AdminPanel(_rolUsuario);
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

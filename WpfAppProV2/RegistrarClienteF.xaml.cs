using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

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

            // Crear carpeta base si no existe
            if (!Directory.Exists(_carpetaBase))
                Directory.CreateDirectory(_carpetaBase);

            // Definir ruta de archivo y crearlo si no existe
            _rutaArchivo = Path.Combine(_carpetaBase, "clientesF.txt");
            if (!File.Exists(_rutaArchivo))
                File.WriteAllText(_rutaArchivo, string.Empty);
        }

        // Mover ventana arrastrando
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Minimizar ventana
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Cerrar ventana (solo esta ventana)
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Volver a ventana padre según rol
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            else if (_rolUsuario.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
                ventanaDestino = new AdminPanel(_rolUsuario);
            else
            {
                MessageBox.Show("Origen desconocido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ventanaDestino.Show();
            this.Close(); // Solo cerrar la ventana de registro
        }

        // Guardar cliente frecuente
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string telefono = txtTelefono.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar nombre (solo letras y espacios)
            Regex regexNombre = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$");
            if (!regexNombre.IsMatch(nombre))
            {
                MessageBox.Show("El nombre solo puede contener letras y espacios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar correo
            Regex regexCorreo = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$");
            if (!regexCorreo.IsMatch(correo))
            {
                MessageBox.Show("Correo inválido. Ingresa un correo válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar teléfono (8-10 dígitos)
            Regex regexTelefono = new Regex(@"^\d{8,10}$");
            if (!regexTelefono.IsMatch(telefono))
            {
                MessageBox.Show("El teléfono debe contener solo números y tener entre 8 y 10 dígitos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int nuevoId = ObtenerNuevoId();
                string linea = $"{nuevoId},{nombre},{correo},{telefono}";
                File.AppendAllText(_rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show("Cliente frecuente registrado correctamente!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cliente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Obtener ID incremental
        private int ObtenerNuevoId()
        {
            int maxId = 0;

            if (File.Exists(_rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(_rutaArchivo);
                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] datos = linea.Split(',');
                    if (datos.Length >= 1 && int.TryParse(datos[0], out int id))
                    {
                        if (id > maxId) maxId = id;
                    }
                }
            }

            return maxId + 1;
        }

        // Limpiar campos del formulario
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
        }
    }
}

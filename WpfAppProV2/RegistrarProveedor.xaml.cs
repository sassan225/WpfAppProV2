using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

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
            string nombre = txtNombreProveedor.Text.Trim();
            string contacto = txtContactoProveedor.Text.Trim();
            string ciudad = txtCiudadProveedor.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contacto) || string.IsNullOrWhiteSpace(ciudad))
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar nombre y ciudad (solo letras y espacios)
            Regex letraRegex = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$");
            if (!letraRegex.IsMatch(nombre))
            {
                MessageBox.Show("El nombre del proveedor solo puede contener letras y espacios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!letraRegex.IsMatch(ciudad))
            {
                MessageBox.Show("La ciudad solo puede contener letras y espacios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar contacto: teléfono (8-10 dígitos) o correo
            Regex telefonoRegex = new Regex(@"^\d{8,10}$");
            Regex correoRegex = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$");
            if (!telefonoRegex.IsMatch(contacto) && !correoRegex.IsMatch(contacto))
            {
                MessageBox.Show("El contacto debe ser un número de teléfono válido (8-10 dígitos) o un correo electrónico válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar duplicados por nombre
            bool existe = false;
            if (File.Exists(_rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(_rutaArchivo);
                for (int i = 0; i < lineas.Length; i++)
                {
                    string linea = lineas[i];
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] datos = linea.Split(',');
                    if (datos.Length >= 2 && datos[1].Trim().ToLower() == nombre.ToLower())
                    {
                        existe = true;
                        break;
                    }
                }
            }

            if (existe)
            {
                MessageBox.Show("Este proveedor ya está registrado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int nuevoId = ObtenerNuevoId();
                string linea = nuevoId + "," + nombre + "," + contacto + "," + ciudad;
                File.AppendAllText(_rutaArchivo, linea + Environment.NewLine);

                MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar proveedor: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Obtener nuevo ID
        private int ObtenerNuevoId()
        {
            int maxId = 0;
            if (File.Exists(_rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(_rutaArchivo);
                for (int i = 0; i < lineas.Length; i++)
                {
                    string linea = lineas[i];
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    string[] datos = linea.Split(',');
                    if (datos.Length >= 1)
                    {
                        int id;
                        if (int.TryParse(datos[0], out id))
                        {
                            if (id > maxId)
                                maxId = id;
                        }
                    }
                }
            }
            return maxId + 1;
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

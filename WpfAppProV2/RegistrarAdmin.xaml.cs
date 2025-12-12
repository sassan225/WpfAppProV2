using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class RegistrarAdmin : Window
    {
        private readonly string rutaAdmins = @"C:\cosmetiqueSoftware\loginsPro.txt";
        private readonly string _rolUsuario;

        public RegistrarAdmin(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            // Crear archivo si no existe
            if (!File.Exists(rutaAdmins))
            {
                File.WriteAllText(rutaAdmins, string.Empty, Encoding.UTF8);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
                return;

            string nombre = txtNombre.Text.Trim();
            string apellidoP = txtApellidoP.Text.Trim();
            string apellidoM = txtApellidoM.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            int anioNacimiento = int.Parse(txtAnioNacimiento.Text.Trim());
            string contrasenia = txtContrasena.Password.Trim();
            string correo = nombre.ToLower() + "." + apellidoP.ToLower() + "@cosmetique.com";
            string rol = "ADMIN";

            try
            {
                // Guardar en el archivo con rol incluido
                string datos = string.Join(",", nombre, apellidoP, apellidoM, telefono, anioNacimiento, contrasenia, rol, correo);
                File.AppendAllText(rutaAdmins, datos + Environment.NewLine, Encoding.UTF8);

                MessageBox.Show("Administrador registrado correctamente!\nCorreo generado: " + correo,
                                "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar admin: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarCampos()
        {
            // Vacíos
            if (txtNombre.Text.Trim() == "" || txtApellidoP.Text.Trim() == "" ||
                txtApellidoM.Text.Trim() == "" || txtTelefono.Text.Trim() == "" ||
                txtAnioNacimiento.Text.Trim() == "" || txtContrasena.Password.Trim() == "")
            {
                MessageBox.Show("Completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Letras y espacios en nombre/apellidos
            Regex letraRegex = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$");
            if (!letraRegex.IsMatch(txtNombre.Text.Trim()) ||
                !letraRegex.IsMatch(txtApellidoP.Text.Trim()) ||
                !letraRegex.IsMatch(txtApellidoM.Text.Trim()))
            {
                MessageBox.Show("Los nombres y apellidos solo pueden contener letras y espacios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Año de nacimiento válido
            int anio;
            if (!int.TryParse(txtAnioNacimiento.Text.Trim(), out anio) || anio < 1940 || anio > DateTime.Now.Year - 10)
            {
                MessageBox.Show("Año de nacimiento inválido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Teléfono válido
            Regex telefonoRegex = new Regex(@"^\d{8,10}$");
            if (!telefonoRegex.IsMatch(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("El teléfono debe contener entre 8 y 10 números.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Contraseña: al menos una mayúscula, una minúscula y un número
            Regex passRegex = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+");
            if (!passRegex.IsMatch(txtContrasena.Password.Trim()))
            {
                MessageBox.Show("La contraseña debe contener mayúscula, minúscula y número.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellidoP.Clear();
            txtApellidoM.Clear();
            txtTelefono.Clear();
            txtAnioNacimiento.Clear();
            txtContrasena.Clear();
            txtCorreo.Clear();
        }

        private void GenerarCorreo()
        {
            if (txtNombre.Text.Trim() != "" && txtApellidoP.Text.Trim() != "")
            {
                txtCorreo.Text = txtNombre.Text.Trim().ToLower() + "." + txtApellidoP.Text.Trim().ToLower() + "@cosmetique.com";
            }
            else
            {
                txtCorreo.Text = "";
            }
        }

        private void txtNombre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            GenerarCorreo();
        }

        private void txtApellidoP_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            GenerarCorreo();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            PanelSuperAdmin panelSuperAdmin = new PanelSuperAdmin(_rolUsuario);
            panelSuperAdmin.Show();
        }
    }
}

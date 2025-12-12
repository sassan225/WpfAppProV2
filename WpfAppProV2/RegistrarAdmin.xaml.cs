using System;
using System.IO;
using System.Text;
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

            if (!File.Exists(rutaAdmins))
            {
                File.WriteAllText(rutaAdmins, string.Empty, Encoding.UTF8);
            }
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtContrasena.Password))
            {
                MessageBox.Show("Completa todos los campos.", "Error");
                return;
            }

            int id = File.ReadAllLines(rutaAdmins).Length + 1;

            string linea = $"{id},{txtNombre.Text},N/A,N/A,N/A,0,{txtContrasena.Password},ADMIN,{txtCorreo.Text}";
            File.AppendAllText(rutaAdmins, linea + Environment.NewLine, Encoding.UTF8);

            MessageBox.Show("Administrador registrado correctamente!", "Éxito");

            txtNombre.Clear();
            txtCorreo.Clear();
            txtContrasena.Clear();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            // Siempre pasamos el rol para mantener consistencia
            PanelSuperAdmin panel = new PanelSuperAdmin(_rolUsuario);
            panel.Show();
            Close();
        }
    }
}

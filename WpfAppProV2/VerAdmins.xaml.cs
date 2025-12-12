using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class VerAdmins : Window
    {
        private readonly string rutaArchivo = @"C:\cosmetiqueSoftware\loginsPro.txt";
        private readonly string _rolUsuario;
        private List<Admin> listaAdmins;

        public VerAdmins(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;
            CargarAdmins();
        }

        private void CargarAdmins()
        {
            listaAdmins = new List<Admin>();

            if (!File.Exists(rutaArchivo))
            {
                File.WriteAllText(rutaArchivo, string.Empty);
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                string[] datos = linea.Split(',');

                if (datos.Length < 7)
                    continue;

                Admin admin = new Admin();
                admin.Nombre = datos[0].Trim();
                admin.ApellidoP = datos[1].Trim();
                admin.ApellidoM = datos[2].Trim();
                admin.Telefono = datos[3].Trim();
                int anio;
                if (!int.TryParse(datos[4].Trim(), out anio))
                    anio = 0;
                admin.AnioNacimiento = anio;
                admin.Contraseña = datos[5].Trim();
                admin.Correo = datos[6].Trim();

                listaAdmins.Add(admin);
            }

            dgAdmins.ItemsSource = null;
            dgAdmins.ItemsSource = listaAdmins;
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarAdmin registrar = new RegistrarAdmin(_rolUsuario);
            registrar.ShowDialog();
            CargarAdmins();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdmins.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un admin para borrar.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Admin adminSeleccionado = (Admin)dgAdmins.SelectedItem;

            MessageBoxResult confirm = MessageBox.Show(
                "¿Seguro que quieres borrar al admin " + adminSeleccionado.NombreCompleto + "?",
                "Confirmar borrado",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirm != MessageBoxResult.Yes)
                return;

            string[] lineas = File.ReadAllLines(rutaArchivo);
            List<string> lineasFiltradas = new List<string>();

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                string[] datos = linea.Split(',');
                if (datos.Length < 7)
                {
                    lineasFiltradas.Add(linea);
                    continue;
                }

                if (datos[6].Trim() != adminSeleccionado.Correo)
                {
                    lineasFiltradas.Add(linea);
                }
            }

            File.WriteAllLines(rutaArchivo, lineasFiltradas.ToArray());
            CargarAdmins();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            PanelSuperAdmin panel = new PanelSuperAdmin(_rolUsuario);
            panel.Show();
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

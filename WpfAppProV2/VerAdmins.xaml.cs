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

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split(',');

                    // Aseguramos que tenga mínimo los campos necesarios y sea ADMIN
                    if (datos.Length >= 8 && datos[6].Trim().ToUpper() == "ADMIN")
                    {
                        Admin admin = new Admin();
                        admin.Nombre = datos[0].Trim() + " " + datos[1].Trim() + " " + datos[2].Trim();
                        admin.Correo = datos[7].Trim();
                        admin.Contraseña = datos[5].Trim();

                        listaAdmins.Add(admin);
                    }
                }
            }

            dgAdmins.ItemsSource = null;
            dgAdmins.ItemsSource = listaAdmins;
        }

        // Abrir ventana de registrar admin
        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarAdmin registrarAdmin = new RegistrarAdmin(_rolUsuario);
            registrarAdmin.ShowDialog();
            CargarAdmins();
        }

        // Borrar admin seleccionado
        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdmins.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un admin para borrar.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Admin adminSeleccionado = (Admin)dgAdmins.SelectedItem;

            MessageBoxResult confirm = MessageBox.Show(
                "¿Seguro que quieres borrar al admin " + adminSeleccionado.Nombre + "?",
                "Confirmar borrado",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (confirm != MessageBoxResult.Yes)
                return;

            List<string> lineasActuales = new List<string>(File.ReadAllLines(rutaArchivo));
            List<string> lineasFiltradas = new List<string>();

            foreach (string linea in lineasActuales)
            {
                string[] datos = linea.Split(',');

                if (datos.Length >= 8 && datos[7].Trim() == adminSeleccionado.Correo)
                    continue; // saltamos la línea del admin a borrar

                lineasFiltradas.Add(linea);
            }

            File.WriteAllLines(rutaArchivo, lineasFiltradas.ToArray());
            CargarAdmins();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            PanelSuperAdmin panelSuperAdmin = new PanelSuperAdmin(_rolUsuario);
            panelSuperAdmin.Show();
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

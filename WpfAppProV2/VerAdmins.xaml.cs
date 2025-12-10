using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class VerAdmins : Window
    {
        private string carpetaBase = @"C:\cosmetiqueSoftware";
        private string rutaArchivo;
        private List<Admin> listaAdmins;

        public VerAdmins()
        {
            InitializeComponent();

            // Crear carpeta si no existe
            if (!Directory.Exists(carpetaBase))
                Directory.CreateDirectory(carpetaBase);

            rutaArchivo = Path.Combine(carpetaBase, "admins.txt");

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
                    string[] datos = linea.Split(',');

                    if (datos.Length == 3)
                    {
                        listaAdmins.Add(new Admin
                        {
                            IdAdmin = int.Parse(datos[0]),
                            Nombre = datos[1],
                            Correo = datos[2]
                        });
                    }
                }
            }

            dgAdmins.ItemsSource = null;
            dgAdmins.ItemsSource = listaAdmins;
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarAdmin registrarAdmin = new RegistrarAdmin();
            registrarAdmin.ShowDialog(); // abrir como modal
            CargarAdmins(); // recargar lista al volver
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdmins.SelectedItem is Admin adminSeleccionado)
            {
                listaAdmins.Remove(adminSeleccionado);
                GuardarAdmins();
                CargarAdmins();
            }
            else
            {
                MessageBox.Show("Selecciona un admin para borrar.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
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

        private void GuardarAdmins()
        {
            List<string> lineas = new List<string>();
            foreach (var admin in listaAdmins)
            {
                lineas.Add($"{admin.IdAdmin},{admin.Nombre},{admin.Correo}");
            }
            File.WriteAllLines(rutaArchivo, lineas);
        }
    }
}
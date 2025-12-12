using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    string[] datos = linea.Split(',');
                    if (datos.Length >= 8 && datos[6].Trim().ToUpper() == "ADMIN")
                    {
                        string nombreCompleto = $"{datos[0]} {datos[1]} {datos[2]}";
                        string correo = datos[7].Trim();
                        string contrasenia = datos[5].Trim();

                        listaAdmins.Add(new Admin
                        {
                            Nombre = nombreCompleto,
                            Correo = correo,
                            Contraseña = contrasenia
                        });
                    }
                }
            }

            dgAdmins.ItemsSource = null;
            dgAdmins.ItemsSource = listaAdmins;
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarAdmin registrarAdmin = new RegistrarAdmin(_rolUsuario); // pasamos rol
            registrarAdmin.ShowDialog();
            CargarAdmins();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdmins.SelectedItem is Admin adminSeleccionado)
            {
                var lineas = File.ReadAllLines(rutaArchivo).ToList();
                lineas = lineas.Where(l =>
                {
                    var datos = l.Split(',');
                    return !(datos.Length >= 8 && datos[7].Trim() == adminSeleccionado.Correo);
                }).ToList();

                File.WriteAllLines(rutaArchivo, lineas);
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
    }
}

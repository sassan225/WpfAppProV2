using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class VerClienteF : Window
    {
        private string carpetaBase = @"C:\cosmetiqueSoftware";
        private string rutaArchivo;
        private List<ClienteF> listaClientes = new List<ClienteF>();

        public VerClienteF()
        {
            InitializeComponent();

            if (!Directory.Exists(carpetaBase))
                Directory.CreateDirectory(carpetaBase);

            rutaArchivo = Path.Combine(carpetaBase, "clientesF.txt");

            CargarClientes();
        }

        private void CargarClientes()
        {
            listaClientes.Clear();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (var linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length == 4)
                    {
                        listaClientes.Add(new ClienteF
                        {
                            IdCliente = int.Parse(datos[0]),
                            Nombre = datos[1],
                            Correo = datos[2],
                         });
                    }
                }
            }

            dgClientes.ItemsSource = null;
            dgClientes.ItemsSource = listaClientes;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            PanelSuperAdmin panel = new PanelSuperAdmin();
            panel.Show();
            this.Close();
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarClienteF registrar = new RegistrarClienteF();
            registrar.Show();
            this.Close();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem is ClienteF seleccionado)
            {
                var result = MessageBox.Show($"¿Deseas borrar a {seleccionado.Nombre}?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    listaClientes.Remove(seleccionado);
                    GuardarClientes();
                    CargarClientes();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente primero.");
            }
        }

        private void GuardarClientes()
        {
            List<string> lineas = new List<string>();

            foreach (var c in listaClientes)
            {
                lineas.Add($"{c.IdCliente},{c.Nombre},{c.Correo}");
            }

            File.WriteAllLines(rutaArchivo, lineas);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

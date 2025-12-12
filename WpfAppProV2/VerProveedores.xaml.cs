using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfAppProV2
{
    public partial class VerProveedores : Window
    {
        private readonly string carpetaBase = @"C:\cosmetiqueSoftware";
        private readonly string rutaArchivo;

        private readonly string _rolUsuario;

        public VerProveedores(string rolUsuario)
        {
            InitializeComponent();
            _rolUsuario = rolUsuario;

            if (!Directory.Exists(carpetaBase))
                Directory.CreateDirectory(carpetaBase);

            rutaArchivo = Path.Combine(carpetaBase, "proveedores.txt");
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length == 4)
                    {
                        Proveedor p = new Proveedor();
                        p.IdProveedor = int.Parse(datos[0]);
                        p.NombreProveedor = datos[1];
                        p.ContactoProveedor = datos[2];
                        p.Ciudad = datos[3];
                        lista.Add(p);
                    }
                }
            }

            dgProveedores.ItemsSource = null; // Evitar problemas de refresco
            dgProveedores.ItemsSource = lista;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaDestino;

            if (_rolUsuario.Equals("superadmin", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new PanelSuperAdmin(_rolUsuario);
            }
            else if (_rolUsuario.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                ventanaDestino = new AdminPanel(_rolUsuario);
            }
            else
            {
                MessageBox.Show("Origen desconocido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ventanaDestino.Show();
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

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProveedor ventana = new RegistrarProveedor(_rolUsuario);
            ventana.ShowDialog();
            CargarProveedores();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProveedores.SelectedItem is Proveedor seleccionado)
            {
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Seguro que deseas eliminar al proveedor '" + seleccionado.NombreProveedor + "'?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    if (File.Exists(rutaArchivo))
                    {
                        string[] lineas = File.ReadAllLines(rutaArchivo);
                        List<string> nuevasLineas = new List<string>();

                        foreach (string linea in lineas)
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length >= 1)
                            {
                                int id = int.Parse(datos[0]);
                                if (id != seleccionado.IdProveedor)
                                {
                                    nuevasLineas.Add(linea);
                                }
                            }
                        }

                        File.WriteAllLines(rutaArchivo, nuevasLineas.ToArray());
                        MessageBox.Show("Proveedor eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarProveedores();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un proveedor para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

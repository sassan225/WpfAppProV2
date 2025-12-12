public class Admin
{
    private static int contadorId = 1;

    public int IdAdmin { get; set; }
    public string Nombre { get; set; }
    public string ApellidoP { get; set; }
    public string ApellidoM { get; set; }
    public string Telefono { get; set; }
    public int AnioNacimiento { get; set; }
    public string Contraseña { get; set; }
    public string Correo { get; set; }

    // Propiedad calculada para mostrar en DataGrid
    public string NombreCompleto => $"{Nombre} {ApellidoP} {ApellidoM}";

    public Admin()
    {
        IdAdmin = contadorId++;
    }

    public override string ToString()
    {
        return $"{Nombre},{ApellidoP},{ApellidoM},{Telefono},{AnioNacimiento},{Contraseña},{Correo}";
    }
}

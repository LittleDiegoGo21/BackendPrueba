namespace MiWebAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Correo { get; set; }
        public decimal Sueldo { get; set; }
        public string? FechaContrato { get; set; }
    }
}

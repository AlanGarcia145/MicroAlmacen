using MicroAlmacen.Modelo;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroAlmacen.Aplicacion
{
    public class AlmacenDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }
        public string NombreAlmacen { get; set; }
        public string Capacidad { get; set; }
        public string Ubicacion { get; set; }
        public string TipoAlmacen { get; set; }
        public string Producto { get; set; }
        public string Encargado { get; set; }
    }
}

using Org.BouncyCastle.Math;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroAlmacen.Modelo
{
    public class Almacen
    {
        [Key]
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

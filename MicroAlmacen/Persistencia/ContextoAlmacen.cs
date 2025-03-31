using MicroAlmacen.Modelo;
using Microsoft.EntityFrameworkCore;

namespace MicroAlmacen.Persistencia
{
    public class ContextoAlmacen : DbContext
    {
        public ContextoAlmacen(DbContextOptions<ContextoAlmacen> options) : base(options)
        {

        }

        public DbSet<Almacen> Almacen { get; set; }

    }
}

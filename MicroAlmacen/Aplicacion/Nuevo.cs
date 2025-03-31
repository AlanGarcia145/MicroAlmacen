using FluentValidation;
using MediatR;
using MicroAlmacen.Modelo;
using MicroAlmacen.Persistencia;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MicroAlmacen.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest<string>
        {
            public string NombreAlmacen { get; set; }
            public string Capacidad { get; set; }
            public string Ubicacion { get; set; }
            public string TipoAlmacen { get; set; }
            public string Producto { get; set; }
            public string Encargado { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.NombreAlmacen).NotEmpty();
                RuleFor(x => x.Capacidad).NotEmpty();
                RuleFor(x => x.Ubicacion).NotEmpty();
                RuleFor(x => x.TipoAlmacen).NotEmpty();
                RuleFor(x => x.Encargado).NotEmpty();
                RuleFor(x => x.Producto).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly ContextoAlmacen _contexto;

            public Manejador(ContextoAlmacen contexto)
            {
                _contexto = contexto;
            }

            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var almacen = new Almacen
                    {
                        NombreAlmacen = request.NombreAlmacen,
                        Capacidad = request.Capacidad,
                        Ubicacion = request.Ubicacion,
                        TipoAlmacen = request.TipoAlmacen,
                        Producto = request.Producto,
                        Encargado = request.Encargado
                    };

                    _contexto.Almacen.Add(almacen);
                    var resultado = await _contexto.SaveChangesAsync(cancellationToken);

                    return resultado > 0 ? "Almacén creado correctamente" : "No se pudo crear el almacén";
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry"))
                    {
                        return "Error: El nombre del almacén ya existe";
                    }
                    return "Error al guardar el almacén";
                }
                catch (Exception)
                {
                    return "Ocurrió un error inesperado";
                }
            }
        }
    }
}

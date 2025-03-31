using AutoMapper;
using MediatR;
using MicroAlmacen.Modelo;
using MicroAlmacen.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace MicroAlmacen.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AlmacenUnico : IRequest<AlmacenDto>
        {
            public string NombreAlmacen { get; set; }
        }

        public class Manejador : IRequestHandler<AlmacenUnico, AlmacenDto>
        {
            private readonly ContextoAlmacen _contextoAlmacen;
            private readonly IMapper _mapper;

            public Manejador(ContextoAlmacen contextoAlmacen, IMapper mapper)
            {
                _contextoAlmacen = contextoAlmacen;
                _mapper = mapper;
            }

            public async Task<AlmacenDto> Handle(AlmacenUnico request, CancellationToken cancellationToken)
            {
                var almacen = await _contextoAlmacen.Almacen
                    .FirstOrDefaultAsync(x => x.NombreAlmacen == request.NombreAlmacen, cancellationToken);

                if (almacen == null)
                {
                    throw new Exception("No se encontró al autor");
                }

                var almacenDto = new AlmacenDto
                {
                    Codigo = almacen.Codigo,
                    NombreAlmacen = almacen.NombreAlmacen,
                    Capacidad = almacen.Capacidad,
                    Ubicacion = almacen.Ubicacion,
                    TipoAlmacen = almacen.TipoAlmacen,
                    Producto = almacen.Producto,
                    Encargado = almacen.Encargado
                };

                return almacenDto;
            }
        }
    }
}

using AutoMapper;
using MediatR;
using MicroAlmacen.Modelo;
using MicroAlmacen.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace MicroAlmacen.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<AlmacenDto>>
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<AlmacenDto>>
        {
            private readonly ContextoAlmacen _contextoAlmacen;
            private readonly IMapper _mapper;

            public Manejador(ContextoAlmacen contextoAlmacen, IMapper mapper)
            {
                _contextoAlmacen = contextoAlmacen;
                _mapper = mapper;
            }

            public async Task<List<AlmacenDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var almacenes = await _contextoAlmacen.Almacen
                    .ToListAsync(cancellationToken);

                var almacenDtos = _mapper.Map<List<AlmacenDto>>(almacenes);
                return almacenDtos;
            }
        }
    }
}

using AutoMapper;
using MicroAlmacen.Modelo;

namespace MicroAlmacen.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Almacen, AlmacenDto>();

        }
    }
}

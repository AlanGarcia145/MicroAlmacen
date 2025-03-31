using MediatR;
using MicroAlmacen.Aplicacion;
using MicroAlmacen.Persistencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroAlmacen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlmacenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Crear([FromBody] Nuevo.Ejecuta data)
        {
            var resultado = await _mediator.Send(data);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> ActualizarAlmacen(int id, [FromBody] Actualizar.Ejecuta data)
        {
            data.Codigo = id;

            var resultado = await _mediator.Send(data);
            return Ok(resultado);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<ActionResult<List<AlmacenDto>>> GetAlmacenes()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpGet("{nombreAlmacen}")]
        public async Task<ActionResult<AlmacenDto>> GetAlmacenUnico(string nombreAlmacen)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return await _mediator.Send(new ConsultaFiltro.AlmacenUnico
            {
                NombreAlmacen = nombreAlmacen
            });
        }

        [HttpDelete("{nombre}")]
        public async Task<ActionResult<bool>> Eleminar(string nombre)
        {
            return await _mediator.Send(new Eliminar.EliminarAlmacen
            {
                NombreAlmacen = nombre
            });
        }
    }
}

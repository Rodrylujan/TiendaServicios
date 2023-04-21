using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompras.Aplicacion;

namespace TiendaServicios.Api.CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarritoCompController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSesionId = id});
        }
    }
}

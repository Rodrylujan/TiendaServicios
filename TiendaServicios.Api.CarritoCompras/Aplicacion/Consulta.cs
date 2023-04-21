using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompras.Persistencia;
using TiendaServicios.Api.CarritoCompras.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompras.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta  : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            public readonly CarritoContexto _contexto;
            public readonly ILibroService _libroService;

            public Manejador(CarritoContexto contexto, ILibroService libroService)
            {
                _contexto = contexto;
                _libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritosesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritosesiondetalle = await _contexto.CarritoSesionDetalles.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                var listCarrotoDto = new List<CarritoDetalleDto>();
                foreach (var libro in carritosesiondetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSelecionado));
                    if (response.resultado)
                    {
                        var obLibro = response.Libro;
                        var carritodetalle = new CarritoDetalleDto { 
                            TituloLibro = obLibro.Titulo,
                            FechaPublicacion = obLibro.FechaPublicacion,
                            LibroId = obLibro.LibreriaMaterialId
                        };
                        listCarrotoDto.Add(carritodetalle);
                    }
                }
                var carritosesiondto = new CarritoDto {
                    CarritoId = carritosesion.CarritoSesionId,
                    FechaCreacionSesion = carritosesion.FechaCreacion,
                    Listaproductos = listCarrotoDto
                };
                return carritosesiondto;
            }
        }
    }
}

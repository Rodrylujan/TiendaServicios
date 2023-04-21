using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompras.Modelo;
using TiendaServicios.Api.CarritoCompras.Persistencia;

namespace TiendaServicios.Api.CarritoCompras.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> productoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritosesion = new CarritoSesion {
                    FechaCreacion = request.FechaCreacionSesion
                };
                _contexto.CarritoSesion.Add(carritosesion);
                var value = await _contexto.SaveChangesAsync();
                if (value==0)
                {
                    throw new Exception("Hay un error en la insersion");
                }
                int id = carritosesion.CarritoSesionId;

                foreach (var bj in request.productoLista)
                {
                    var detallesesion = new CarritoSesionDetalle { 
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSelecionado = bj
                    };
                    _contexto.CarritoSesionDetalles.Add(detallesesion);
                }
                value = await _contexto.SaveChangesAsync();
                if (value>0) 
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo ingresar el detalle del carrito de compras");

            }
        }
    }
}

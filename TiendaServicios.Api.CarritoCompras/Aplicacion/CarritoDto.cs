using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.CarritoCompras.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> Listaproductos { get; set; }

    }
}

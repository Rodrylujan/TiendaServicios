using System;

namespace TiendaServicios.Api.CarritoCompras.Modelo
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSelecionado { get; set; }

        public int CarritoSesionId { get; set; }
        public CarritoSesion carritosesion{get; set;}
    }
}

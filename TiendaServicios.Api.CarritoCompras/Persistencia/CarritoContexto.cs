using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompras.Modelo;

namespace TiendaServicios.Api.CarritoCompras.Persistencia
{
    public class CarritoContexto : DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options) { }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
    }
}

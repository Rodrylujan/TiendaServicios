using System;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompras.RemoteModel;

namespace TiendaServicios.Api.CarritoCompras.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado,LibroRemote Libro,string ErrorMessage)> GetLibro(Guid LibroId);
    }
}

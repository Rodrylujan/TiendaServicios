using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }
        public class EjecutaValidadcion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidadcion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(X => X.Apellido).NotEmpty(); 
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro { 
                Nombre = request.Nombre,
                FechaNacimiento = request.FechaNacimiento,
                Apellido = request.Apellido,
                AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };
                _contexto.AutorLibro.Add(autorLibro);
                var valor = await _contexto.SaveChangesAsync();
                if (valor>0)
                {
                    return Unit.Value;
                }
                throw new Exception("Nos e pudo ingresar el libro");
            }
        }
    }
}

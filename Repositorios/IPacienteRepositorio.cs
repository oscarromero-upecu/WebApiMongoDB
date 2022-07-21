using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppMongoDBUClinica.Colecciones;
using WebAppMongoDBUClinica.Request;

namespace WebAppMongoDBUClinica.Repositorios
{
    public interface IPacienteRepositorio
    {
        Task<paciente> ConsultarPaciente (string id);
        Task<IEnumerable<paciente>> ConsultarPacientes ();
        Task<IEnumerable<paciente>> ObtenerPaciente(string nombreBuscar, string cedulaBuscar);
        Task IngresarPaciente(paciente request);
        Task <bool> ActualizarPaciente(paciente request);
        Task EliminarPaciente(string id);
        Task<bool> Activar(string id);
        Task<bool> Desactivar(string id);

    }
}

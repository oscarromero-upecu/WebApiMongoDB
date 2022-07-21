using MongoDB.Driver;
using WebAppMongoDBUClinica.Colecciones;

namespace WebAppMongoDBUClinica.Datos.Interfaces
{
    public interface IClinicaContext
    {
        MongoClient mongoClient { get; }
        IMongoCollection<paciente> Paciente { get; set; }

    }
}

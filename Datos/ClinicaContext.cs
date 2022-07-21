using MongoDB.Driver;
using WebAppMongoDBUClinica.Colecciones;
using WebAppMongoDBUClinica.Configuraciones;
using WebAppMongoDBUClinica.Datos.Interfaces;

namespace WebAppMongoDBUClinica.Datos
{
    public class ClinicaContext : IClinicaContext
    {
        public ClinicaContext(ClinicaDBSettings settings)
        {
            mongoClient = new MongoClient(settings.ConnectionString);
            var db = mongoClient.GetDatabase(settings.Database);
            Paciente = db.GetCollection<paciente>(settings.Collectionpaciente);
        }
        public MongoClient mongoClient { get; }
        public IMongoCollection<paciente> Paciente { get; set; }
    }
}

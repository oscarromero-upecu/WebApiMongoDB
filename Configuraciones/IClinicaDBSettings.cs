namespace WebAppMongoDBUClinica.Configuraciones
{
    public interface IClinicaDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collectionpaciente { get; set; }

    }
}

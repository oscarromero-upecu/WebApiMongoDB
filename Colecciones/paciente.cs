using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebAppMongoDBUClinica.Colecciones
{
    public class paciente
    {
        //metodos que congifura la ID de mongo
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NombreApellido { get; set; }
        public string CedulaPasaporte { get; set; }
        public string TipoDeSangre { get; set; }

        //metodos que congifura la fecha de mongo
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaDeIngreso { get; set; }
        public string Diagnostico { get; set; }
        public bool Activo { get; set; }

    }
}

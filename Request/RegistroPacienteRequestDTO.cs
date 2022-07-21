using System;

namespace WebAppMongoDBUClinica.Request
{
    public class RegistroPacienteRequestDTO
    {
        public string NombreApellido { get; set; }
        public string CedulaPasaporte { get; set; }
        public string TipoDeSangre { get; set; }

        //metodos que congifura la fecha de mongo
        //public DateTime FechaDeIngreso { get; set; }
        public string Diagnostico { get; set; }
    }
}

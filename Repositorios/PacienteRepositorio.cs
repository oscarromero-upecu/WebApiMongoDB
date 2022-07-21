using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAppMongoDBUClinica.Colecciones;
using WebAppMongoDBUClinica.Datos.Interfaces;
using WebAppMongoDBUClinica.Request;

namespace WebAppMongoDBUClinica.Repositorios
{
    public class PacienteRepositorio : IPacienteRepositorio
    {
        private readonly IClinicaContext _dbContext;

        public PacienteRepositorio(IClinicaContext dbContext)
        {
            _dbContext = dbContext??throw new ArgumentException(nameof(dbContext));
        }
        #region CRUD MONGODB
        public async Task  IngresarPaciente(paciente request)
        {
            await _dbContext.Paciente.InsertOneAsync(request);
        }
        public async Task <bool> ActualizarPaciente(paciente pacienteEditar)
        {
            var filter = Builders<paciente>.Filter.Eq(p => p.Id, pacienteEditar.Id);
            var update = Builders<paciente>.Update.Set(p => p.NombreApellido, pacienteEditar.NombreApellido)
                                                .Set(p => p.CedulaPasaporte, pacienteEditar.CedulaPasaporte)
                                                .Set(p => p.TipoDeSangre, pacienteEditar.TipoDeSangre)
                                                .Set(p => p.Diagnostico, pacienteEditar.Diagnostico);

            var updateResult = await _dbContext.Paciente.UpdateOneAsync(filter, update);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }

        public async Task<paciente> ConsultarPaciente(string id)
        {
            return await  _dbContext.Paciente
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<paciente>> ConsultarPacientes()
        {

            return await _dbContext.Paciente.Find(p => p.Id != null).ToListAsync();
           
        }

        public async Task<IEnumerable<paciente>> ObtenerPaciente(string nombreBuscar, string cedulaBuscar)
        {
            var queryExpression = new BsonRegularExpression(new Regex(nombreBuscar, RegexOptions.IgnoreCase));
            var filterNombre = Builders<paciente>.Filter.Regex(r => r.NombreApellido, queryExpression);

            var queryExpressionCedula = new BsonRegularExpression(new Regex(cedulaBuscar, RegexOptions.IgnoreCase));
            var filterCedula = Builders<paciente>.Filter.Regex(r => r.CedulaPasaporte, queryExpressionCedula);

            var filter = Builders<paciente>.Filter.Or(filterNombre, filterCedula);

            return await _dbContext.Paciente.Find(filter).ToListAsync();
        }

        public async Task EliminarPaciente(string id)
        {
            var filter = Builders<paciente>.Filter.Eq(p => p.Id , id);
           return  await _dbContext.Paciente.DeleteOneAsync(filter);

           
         }

      
        #endregion

        #region ACTIVAR o DESACTIVAR PACIENTES
        public async Task<bool> Activar(string id)
        {
            var filter = Builders<paciente>.Filter.Eq(p => p.Id, id);
            var update = Builders<paciente>.Update.Set(p => p.Activo, true);

            var updateResult = await _dbContext.Paciente.UpdateOneAsync(filter, update);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Desactivar(string id)
        {
            var filter = Builders<paciente>.Filter.Eq(p => p.Id, id);
            var update = Builders<paciente>.Update.Set(p => p.Activo, false);

            var updateResult = await _dbContext.Paciente.UpdateOneAsync(filter, update);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        #endregion
    }
}

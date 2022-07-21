using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebAppMongoDBUClinica.Colecciones;
using WebAppMongoDBUClinica.Repositorios;
using WebAppMongoDBUClinica.Request;

namespace WebAppMongoDBUClinica.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepositorio _pacienteRepositorio;
        private readonly ILogger<PacienteController> _logger;

        public PacienteController(IPacienteRepositorio pacienteRepositorio, ILogger<PacienteController> logger)
        {
            _pacienteRepositorio = pacienteRepositorio;
            _logger = logger;
        }

        // METODOS GET
        #region GET
        [HttpGet]
        public async Task<IActionResult> ConsultarPaciente()
        {
            return Ok(await _pacienteRepositorio.ConsultarPacientes());
        }

        [HttpGet("ConsultarPaciente_Por_ID")]
        public async Task<IActionResult> ConsultarPacientePorID(string id)
        {
            return Ok(await _pacienteRepositorio.ConsultarPaciente(id));
        }
        [HttpGet("BuscarPaciente_por_Nombre_Cedula")]
        [ProducesResponseType(typeof(IEnumerable<paciente>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<paciente>>> BuscarPaciente(string nombre, string cedula)
        {
            var Paciente = await _pacienteRepositorio.ObtenerPaciente(nombre, cedula);
            return Ok(Paciente);
        }
        #endregion

        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actulizar([FromBody] paciente request)
        {
            if (await _pacienteRepositorio.ActualizarPaciente(request))
            {
                return Ok("Paciente Actualizado");
            }
            else
            {
                _logger.LogError($"Paciente {request.Id} no existe o no hay valores por actualizar");
                return BadRequest("Paciente no existe o no hay valores por actualizar");
            }
        }
        //METODOS POST (GUARDAR O ENVIAR DATOS)
        #region POST
        [HttpPost("IngresarPaciente")]
        public async Task<IActionResult> IngresarPaciente([FromBody] paciente request)
        {
            try
            {
                if (request is null || !ModelState.IsValid)
                    return BadRequest("Los campos estan incompletos"); //retorna que es una solicitud mala
                await _pacienteRepositorio.IngresarPaciente(request);
                return Ok("Paciente ingresado");
            }
            catch (Exception)
            {

                return BadRequest("Error de controlador"); //retorna que es una solicitud mala
            }

        }
        #endregion

        [HttpDelete("Eliminar_registro")]
        public async Task<ActionResult> Eliminar(string id)
        {
            return Ok(await _pacienteRepositorio.EliminarPaciente(id));
        }


        [HttpPatch("Desactivar")]
        public async Task<ActionResult> Desactivar(string id)
        {
            if (await _pacienteRepositorio.Desactivar(id))
            {
                return Ok("Paciente desactivado");
            }
            else
            {
                _logger.LogError($"Paciente {id} no existe o esta desactivado");
                return BadRequest("Paciente no existe o ya esta desactivado");
            }
        }

        [HttpPatch("Activar")]
        public async Task<ActionResult> Activar(string id)
        {
            if (await _pacienteRepositorio.Activar(id))
            {
                return Ok("Paciente activado");
            }
            else
            {
                _logger.LogError($"Paciente {id} no existe o esta activado");
                return BadRequest("Paciente no existe o ya esta activado");
            }
        }
    }
}

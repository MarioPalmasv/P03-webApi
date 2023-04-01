using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P03_webApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P03_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public estados_equipoController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }


        [HttpGet]
        [Route("GetAll")]

        //SELECT
        public IActionResult Get()
        {
            List<estados_equipo> listaEstadosEquipo = (from e in _equiposContext.estados_equipo
                                                        select e).ToList();
            if (listaEstadosEquipo.Count == 0) { return NotFound(); }

            return Ok(listaEstadosEquipo);
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] estados_equipo estado_equipo_nuevo) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _equiposContext.estados_equipo.Add(estado_equipo_nuevo);
                _equiposContext.SaveChanges();

                return Ok(estado_equipo_nuevo); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult actualizar(int id, [FromBody] estados_equipo estadoEquipoActualizar)
        {
            try
            {
                estados_equipo? estadosExist = (from e in _equiposContext.estados_equipo
                                                   where e.id_estados_equipo == id
                                                   select e).FirstOrDefault();

                if (estadosExist == null) { return NotFound(); }

                estadosExist.descripcion = estadoEquipoActualizar.descripcion;
                estadosExist.estado = estadoEquipoActualizar.estado;

                _equiposContext.Entry(estadosExist).State = EntityState.Modified;
                _equiposContext.SaveChanges();

                return Ok(estadoEquipoActualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult borrar(int id)
        {
            try
            {
                estados_equipo? estadoEliminar = (from e in _equiposContext.estados_equipo
                                                      where e.id_estados_equipo == id
                                                      select e).FirstOrDefault();

                if (estadoEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _equiposContext.estados_equipo.Attach(estadoEliminar); //para apuntar cual de todos vamos a eliminar
                _equiposContext.estados_equipo.Remove(estadoEliminar);
                _equiposContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P03_webApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P03_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public tipo_equipoController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }


        [HttpGet]
        [Route("GetAll")]

        //SELECT
        public IActionResult Get()
        {
            List<tipo_equipo> tipoEquipo = (from e in _equiposContext.tipo_equipo
                                                       select e).ToList();
            if (tipoEquipo.Count == 0) { return NotFound(); }

            return Ok(tipoEquipo);
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] tipo_equipo tipo_equipo_nuevo) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _equiposContext.tipo_equipo.Add(tipo_equipo_nuevo);
                _equiposContext.SaveChanges();

                return Ok(tipo_equipo_nuevo); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult actualizar(int id, [FromBody] tipo_equipo tipoEquipoActualizar)
        {
            try
            {
                tipo_equipo? tipoEquipoExist = (from e in _equiposContext.tipo_equipo
                                                where e.id_tipo_equipo == id
                                                select e).FirstOrDefault();

                if (tipoEquipoExist == null) { return NotFound(); }

                tipoEquipoExist.descripcion = tipoEquipoActualizar.descripcion;
                tipoEquipoExist.estado = tipoEquipoActualizar.estado;

                _equiposContext.Entry(tipoEquipoExist).State = EntityState.Modified;
                _equiposContext.SaveChanges();

                return Ok(tipoEquipoActualizar);
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
                tipo_equipo? tipoEquipoEliminar = (from e in _equiposContext.tipo_equipo
                                                  where e.id_tipo_equipo == id
                                                  select e).FirstOrDefault();

                if (tipoEquipoEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _equiposContext.tipo_equipo.Attach(tipoEquipoEliminar); //para apuntar cual de todos vamos a eliminar
                _equiposContext.tipo_equipo.Remove(tipoEquipoEliminar);
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

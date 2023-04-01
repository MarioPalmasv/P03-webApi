using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P03_webApi.Models;
namespace P03_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public marcasController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        //SELECT
        public IActionResult Get()
        {
            List<marcas> listaMarcas = (from e in _equiposContext.marcas
                                                       select e).ToList();
            if (listaMarcas.Count == 0) { return NotFound(); }

            return Ok(listaMarcas);
        }

        [HttpPost] //Create o insert
        [Route("add")]

        public IActionResult crear([FromBody] marcas marcas_nuevo) //le ponemos Frombody para que lo busque en el código
        {
            try
            {
                _equiposContext.marcas.Add(marcas_nuevo);
                _equiposContext.SaveChanges();

                return Ok(marcas_nuevo); // tiene sentido regresar este objeto para corroborar el insert que se quedo en la d◘

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]

        public IActionResult actualizar(int id, [FromBody] marcas marcaActualizar)
        {
            try
            {
                marcas? marcaExist = (from e in _equiposContext.marcas
                                                where e.id_marca == id
                                                select e).FirstOrDefault();

                if (marcaExist == null) { return NotFound(); }

                marcaExist.nombre_marca = marcaActualizar.nombre_marca;
                marcaExist.estados = marcaActualizar.estados;

                _equiposContext.Entry(marcaExist).State = EntityState.Modified;
                _equiposContext.SaveChanges();

                return Ok(marcaActualizar);
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
                marcas? marcasEliminar = (from e in _equiposContext.marcas
                                                  where e.id_marca == id
                                                  select e).FirstOrDefault();

                if (marcasEliminar == null) { return NotFound(); }

                ////Esto se hace para eliminar los registros cosa que no se debe hacer

                _equiposContext.marcas.Attach(marcasEliminar); //para apuntar cual de todos vamos a eliminar
                _equiposContext.marcas.Remove(marcasEliminar);
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

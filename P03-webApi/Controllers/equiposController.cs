using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P03_webApi.Models;
using Microsoft.EntityFrameworkCore;

namespace P03_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContext;

        public equiposController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            //List<equipos> listaEquipos = (from e in _equiposContext.equipos
            //                              select e).ToList();

            //if (listaEquipos.Count == 0)
            //{
            //    return NotFound();
            //}

            //return Ok(listaEquipos);

            //var listaEquipos = (from e in _equiposContext.equipos
            //                               join t in _equiposContext.tipo_equipo
            //                                      on e.tipo_equipo_id equals t.id_tipo_equipo
            //                               join m in _equiposContext.marcas
            //                                      on e.marca_id equals m.id_marca
            //                               join es in _equiposContext.estados_equipo
            //                                      on e.estado_equipo_id equals es.id_estados_equipo
            //                               select new
            //                               {
            //                                   e.id_equipos,
            //                                   e.nombre,
            //                                   e.descripcion,
            //                                   e.tipo_equipo_id,
            //                                   tipo_equipo = t.descripcion,
            //                                   e.marca_id,
            //                                   marca = m.nombre_marca,
            //                                   e.estado_equipo_id,
            //                                   estado_equipo = es.descripcion,
            //                                   detalle = $"Tipo : {t.descripcion}, Marca : {m.nombre_marca}, Estado equipo : {es.descripcion}",
            //                                   e.estado
            //                               }).Skip(1).Take(1).ToList();

            var listaEquipos = (from e in _equiposContext.equipos
                                join t in _equiposContext.tipo_equipo
                                       on e.tipo_equipo_id equals t.id_tipo_equipo
                                join m in _equiposContext.marcas
                                       on e.marca_id equals m.id_marca
                                join es in _equiposContext.estados_equipo
                                       on e.estado_equipo_id equals es.id_estados_equipo
                                select new
                                {
                                    e.id_equipos,
                                    e.nombre,
                                    e.descripcion,
                                    e.tipo_equipo_id,
                                    tipo_equipo = t.descripcion,
                                    e.marca_id,
                                    marca = m.nombre_marca,
                                    e.estado_equipo_id,
                                    estado_equipo = es.descripcion,
                                    detalle = $"Tipo : {t.descripcion}, Marca : {m.nombre_marca}, Estado equipo : {es.descripcion}",
                                    e.estado
                                }).OrderBy(resultado => resultado.estado_equipo_id).
                                   ThenBy(resultado => resultado.marca_id).
                                   ThenByDescending(resultado => resultado.tipo_equipo_id).ToList();

            if (listaEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaEquipos);
        }

    }
}

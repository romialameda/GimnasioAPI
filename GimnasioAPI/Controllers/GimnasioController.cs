using GimnasioAPI.Adapter;
using GimnasioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("[controller]/api/[action]")]
    public class GimnasioController : Controller
    {
        [HttpGet]
        [ActionName("ObtenerGimnasiosCompleto")]

        public ActionResult<List<Gimnasio>> ObtenerGimnasiosCompleto()
        {
            GeneralAdapterSQL consultor = new GeneralAdapterSQL();
            DataTable respuesta = consultor.EjecutarVista("Gimnasios");

            //Conflict - 409 - Error interno
            if (respuesta.Rows.Count > 0 && respuesta.Rows.ToString()?.Trim() == "ERROR")
            {
                return Conflict("Ocurrio un error en la base de datos.");
            }

            //No Content - 204 - Tabla vacia
            if (respuesta.Rows.Count == 0)
            {
                return NoContent(); 
            }

            List<Gimnasio> listadoGimnasioCompleto = new List<Gimnasio>();

            try
            {
                foreach (DataRow row in respuesta.Rows)
                {
                    listadoGimnasioCompleto.Add(new Gimnasio(row));
                }
                //Ok - 200
                return Ok(listadoGimnasioCompleto);
            }
            catch (Exception ex) 
            {
                // 500 - Internal Server Error o 400 - Errores inesperados
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
            
        }

        [HttpGet]
        [ActionName("ObtenerGimnasiosActivo")]

        public ActionResult<List<Gimnasio>> ObtenerGimnasiosActivo()
        {
            GeneralAdapterSQL consultor = new GeneralAdapterSQL();
            DataTable respuesta = consultor.EjecutarVista("Vista_ObtenerGimnasiosActivo");

            //409
            if (respuesta.Rows.Count > 0 && respuesta.Rows.ToString()?.Trim() == "ERROR")
            {
                return Conflict("Error en la base de datos");
            }

            //204
            if (respuesta.Rows.Count == 0) return NoContent();

            List<Gimnasio> listadoGimnasiosActivo = new List<Gimnasio>();

            try 
            { 
                foreach (DataRow row in respuesta.Rows)
                {
                    listadoGimnasiosActivo.Add(new Gimnasio(row));
                }

                return Ok(listadoGimnasiosActivo);
            }
            catch (Exception ex) {
                //500
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ActionName("ObtenerGimnasioXid")]
        public ActionResult<Gimnasio> ObtenerGimnasioXid(int id)
        {
            //400
            if (id <= 0)
            {
                return BadRequest("El ID debe ser un numero mayor o igual a cero(0).");
            }

            try 
            {
                GeneralAdapterSQL consultor = new GeneralAdapterSQL();
                DataTable respuesta = consultor.EjecutarProcedimiento("ProcObtenerGimnasioPorId", 
                    new Dictionary<string, object>
                    {
                    {"@id_gimnasio", id }
                    });

                if (respuesta.Rows.Count > 0)
                {
                    if (respuesta.Rows.ToString()?.Trim() == "ERROR") return Conflict("Error en la base de datos");
                    else
                    {
                        Gimnasio gimnasioId = new Gimnasio(respuesta.Rows[0]);
                        return Ok(gimnasioId);
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex) { return StatusCode(500, "Error interno del servidor: " + ex.Message); }
        }

        [HttpPost]
        [ActionName("CargarGimnasio")]
        public ActionResult<Gimnasio> CargarGimnasio([FromBody] Gimnasio nuevoGimnasio)
        {
            GeneralAdapterSQL consultor = new GeneralAdapterSQL();
            DataTable respuesta = consultor.EjecutarProcedimiento("CargarGimnasio", new Dictionary<string, object>
                { { "@ciudad_gimnasio", nuevoGimnasio.ciudad_gimnasio},
                  { "@id_region", nuevoGimnasio.id_region },
                  { "@fecha_alta", nuevoGimnasio.fecha_alta },
                  { "@entrenador_lider", nuevoGimnasio.entrenador_lider },
                  { "@gimnasio_activo", nuevoGimnasio.gimnasio_activo },
                  { "@nombre_medalla", nuevoGimnasio.nombre_medalla}

            });

            try { 
                if (respuesta.Rows.Count > 0)
                {
                    if (respuesta.Rows.ToString()?.Trim() == "ERROR") return Conflict("Error en la base de datos");
                    else
                    {
                        Gimnasio gimnasioCreado = new Gimnasio(respuesta.Rows[0]);
                        return Created("Gimnasio creado: ", gimnasioCreado);
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex) { return StatusCode(500, "Error interno del servidor: " + ex.Message); }
        }
    }
}
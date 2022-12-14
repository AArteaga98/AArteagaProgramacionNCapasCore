using BL;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("GetAll")] //Se tiene que agregar el GetAll para que entre al metodo
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetById(idUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("GetAll")]
        public IActionResult GetAll( byte idRol  ,string? nombre, string? ap)
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
           
            usuario.Nombre = (nombre == null) ? "" : nombre;
            usuario.ApellidoPaterno = (ap == null) ? "" : ap;
            usuario.Rol.IdRol = (byte)((idRol == null) ? 0 : idRol);



            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST api/<UsuarioController>
        [HttpPost("Add")]
        public IActionResult Post([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update/{idUsuario}")]
        public IActionResult Put(int IdUsuario, [FromBody] ML.Usuario usuario)
        {
            
            usuario.IdUsuario= IdUsuario;
            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{IdUsuario}")]
        //[Route("api/Usuario/Delete/{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.Delete(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }


    }
}


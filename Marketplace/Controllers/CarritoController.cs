using Marketplace.Entities;
using Marketplace.Models;
using Marketplace.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private ServicioCarrito srvCarrito;
        public CarritoController()
        {
            srvCarrito = new ServicioCarrito();
        }

        [HttpPost("Agregar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Agregar(CarritoModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                var carrito = new Carrito();
                carrito.Id = model.Id;
                carrito.Cantidad = model.Cantidad;
                carrito.IdProducto = model.IdProducto;
                carrito.IdUsuario = int.Parse(idUsuario);

                srvCarrito.Guardar(carrito);
            }
            catch (Exception)
            {
                return BadRequest("Error al guardar el item del carrito");
            }

            return Ok("Item agregado con éxito");
        }

        [HttpDelete("Eliminar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Eliminar(long id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                srvCarrito.Eliminar(id);
            }
            catch (Exception)
            {
                return BadRequest("Error al eliminar el item del carrito");
            }

            return Ok("Item eliminado con éxito");
        }

        [HttpGet("Buscar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Buscar()
        {
            var carrito = new List<Carrito>();
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                carrito = srvCarrito.Buscar(int.Parse(idUsuario));
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar el carrito: " + ex.Message);
            }

            return Ok(carrito);
        }

        [HttpGet("Buscar/{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult Buscar(int id)
        {
            var carrito = new List<Carrito>();
            try
            {
                carrito = srvCarrito.Buscar(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar el carrito: " + ex.Message);
            }

            return Ok(carrito);
        }
    }
}

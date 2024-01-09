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
    public class CompraController : ControllerBase
    {
        private ServicioCompra srvCompra;
        public CompraController()
        {
            srvCompra = new ServicioCompra();
        }

        [HttpPost("Alta")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Alta(List<CarritoModel> model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                var compra = new Compra();
                compra.IdUsuario = int.Parse(idUsuario);
                compra.Fecha = DateTime.Now;

                var detalleCompras = new List<DetalleCompra>();
                foreach(var detalle in model)
                {
                    var det = new DetalleCompra();
                    det.IdProducto = detalle.IdProducto;
                    det.Cantidad = detalle.Cantidad;                    

                    detalleCompras.Add(det);
                }

                srvCompra.Guardar(compra, detalleCompras);
            }
            catch (Exception)
            {
                return BadRequest("Error al generar compra");
            }

            return Ok("Compra generada con éxito");
        }

        [HttpGet("Buscar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Buscar()
        {
            var compras = new List<Compra>();
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                compras = srvCompra.Buscar(int.Parse(idUsuario));
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar compras: " + ex.Message);
            }

            return Ok(compras);
        }

        [HttpGet("Buscar/{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult Buscar(int id)
        {
            var compras = new List<Compra>();
            try
            {
                compras = srvCompra.Buscar(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar compras: " + ex.Message);
            }

            return Ok(compras);
        }
    }
}

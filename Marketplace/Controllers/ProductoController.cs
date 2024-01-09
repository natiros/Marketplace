using Entities;
using Marketplace.Entities;
using Marketplace.Models;
using Marketplace.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private ServicioProducto srvProducto;
        public ProductoController()
        {
            srvProducto = new ServicioProducto();
        }

        [HttpPost("Alta")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Alta(ProductoModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                var producto = new Producto();
                producto.Nombre = model.Nombre;
                producto.Descripcion = model.Descripcion;
                producto.Precio = model.Precio;
                producto.Stock = model.Stock;
                producto.Activo = model.Activo;
                producto.Archivo = model.Archivo;
                producto.IdUsuario = int.Parse(idUsuario);

                srvProducto.Guardar(producto);
            }
            catch (Exception)
            {
                return BadRequest("Error al guardar el producto");
            }

            return Ok("Producto guardado con éxito");
        }

        [HttpPut("Modificar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Modificar(ProductoModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                var producto = new Producto();
                producto.Id = model.Id;
                producto.Nombre = model.Nombre;
                producto.Descripcion = model.Descripcion;
                producto.Precio = model.Precio;
                producto.Stock = model.Stock;
                producto.Activo = model.Activo;
                producto.Archivo = model.Archivo;
                producto.IdUsuario = int.Parse(idUsuario);

                srvProducto.Modificar(producto);
            }
            catch (Exception)
            {
                return BadRequest("Error al guardar el producto");
            }

            return Ok("Producto modificado con éxito");
        }

        [HttpPut("Cancelar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Cancelar(ProductoTransaccionModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                srvProducto.Cancelar(model.Id, int.Parse(idUsuario));
            }
            catch (Exception)
            {
                return BadRequest("Error al cancelar el producto");
            }

            return Ok("Producto cancelado con éxito");
        }

        [HttpPut("Pausar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Pausar(ProductoTransaccionModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                srvProducto.Pausar(model.Id, int.Parse(idUsuario));
            }
            catch (Exception)
            {
                return BadRequest("Error al pausar el producto");
            }

            return Ok("Producto pausado con éxito");
        }

        [HttpPut("Relanzar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Relanzar(RelanzarModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = identity.FindFirst(ClaimTypes.Sid).Value;

                srvProducto.Relanzar(model.Id, model.Stock, int.Parse(idUsuario));
            }
            catch (Exception ex)
            {
                return BadRequest("Error al relanzar el producto: " + ex.Message);
            }

            return Ok("Producto relanzado con éxito");
        }

        [HttpGet("Buscar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Buscar(string? nombre, string? descripcion, decimal? precioMin, decimal? precioMax)
        {
            var valoresStringVacios = string.IsNullOrEmpty(nombre) && string.IsNullOrEmpty(descripcion);
            var valoresDecimalNulos = precioMin == null && precioMax == null;

            if (valoresStringVacios && valoresDecimalNulos)
            {
                return BadRequest("Debe llenar al menos un filtro");
            }

            IEnumerable<Producto> productos = new List<Producto>();

            try
            {
               productos = srvProducto.Buscar(nombre, descripcion, precioMin, precioMax);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar productos: " + ex.Message);
            }

            return Ok(productos);
        }

        [HttpGet("BuscarTodosActivos")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult BuscarTodosActivos()
        {
            IEnumerable<Producto> productos = new List<Producto>();

            try
            {
                productos = srvProducto.BuscarTodosActivos();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar productos: " + ex.Message);
            }

            return Ok(productos);
        }

        [HttpGet("BuscarDetalle/{id}")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult BuscarPorId(int id)
        {
            var producto = new Producto();

            try
            {
                producto = srvProducto.BuscarPorId(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar productos: " + ex.Message);
            }

            return Ok(producto);
        }

        [HttpGet("BuscarTodos")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult BuscarTodos()
        {
            var productos = new List<Producto>();
            try
            {
                productos = (List<Producto>)srvProducto.BuscarTodos();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar productos: " + ex.Message);
            }

            return Ok(productos);
        }


        [HttpPut("CancelarProducto")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult CancelarProducto(ProductoTransaccionModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                srvProducto.Cancelar(model.Id);
            }
            catch (Exception)
            {
                return BadRequest("Error al cancelar el producto");
            }

            return Ok("Producto cancelado con éxito");
        }
    }
}

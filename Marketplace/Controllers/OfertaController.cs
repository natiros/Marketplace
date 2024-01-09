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
    public class OfertaController : ControllerBase
    {
        private ServicioOferta srvOferta;
        public OfertaController()
        {
            srvOferta = new ServicioOferta();
        }

        [HttpPost("Alta")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Alta(OfertaModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var oferta = new Oferta();
                oferta.Nombre = model.Nombre;
                oferta.FechaInicio = model.FechaInicio;
                oferta.FechaFin = model.FechaFin;

                var detalleOfertas = new List<DetalleOferta>();
                foreach(var producto in model.Productos)
                {
                    var detalleOferta = new DetalleOferta();
                    detalleOferta.IdProducto = producto.Id;
                    detalleOferta.PorcentajeDescuento = producto.PorcentajeDescuento;

                    detalleOfertas.Add(detalleOferta);
                }

                srvOferta.Guardar(oferta, detalleOfertas);
            }
            catch (Exception)
            {
                return BadRequest("Error al guardar la oferta");
            }

            return Ok("Oferta guardado con éxito");
        }

        [HttpGet("Buscar")]
        [Authorize(Roles = "USUARIO,ADMINISTRADOR")]
        public ActionResult Buscar()
        {
            var ofertas = new List<Oferta>();
            try
            {
               ofertas = srvOferta.Buscar();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar la oferta: " + ex.Message);
            }

            return Ok(ofertas);
        }


        [HttpPut("Cancelar")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult Cancelar(OfertaTransaccionModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                srvOferta.Cancelar(model.Id);
            }
            catch (Exception)
            {
                return BadRequest("Error al cancelar la oferta");
            }

            return Ok("Oferta cancelada con éxito");
        }

        [HttpGet("BuscarTodas")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult BuscarTodas()
        {
            var ofertas = new List<Oferta>();
            try
            {
                ofertas = srvOferta.BuscarTodasOfertas();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar ofertas: " + ex.Message);
            }

            return Ok(ofertas);
        }
    }
}

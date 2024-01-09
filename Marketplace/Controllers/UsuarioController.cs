using Entities;
using Marketplace.Configuracion;
using Marketplace.Models;
using Marketplace.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marketplace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private ServicioUsuario srvUsuario;
        private JwtConfiguracion jwtConfig;
        public UsuarioController(IOptions<JwtConfiguracion> jwtConfiguracion)
        {
            srvUsuario = new ServicioUsuario();
            jwtConfig = jwtConfiguracion.Value;
        }

        [HttpPost("Login")]
       public ActionResult Login(LoginModel user)
       {
            var usuario = srvUsuario.BuscarUsuarioPorEmail(user.Email);

            if (!PasswordHelper.VerificarPassword(user.Password, usuario.Password))
                return Unauthorized("Contraseña incorrecta");

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, usuario.ID.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario)
            };

            var sectoken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims:claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(sectoken);

            return Content(token);
       }

        [HttpPost("Alta")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult Alta(UsuarioModel model) 
        {
            if (model == null)
                return BadRequest();

            try{ 
                var user = new Usuario();
                user.Nombre = model.Nombre;
                user.Apellido = model.Apellido;
                user.Email = model.Email;
                user.Password = model.Password;
                user.IdTipoUsuario = model.TipoUsuario;

                srvUsuario.GuardarUsuario(user);
            }
            catch(Exception)
            {   
                return BadRequest("Error al dar de alta el usuario");
            }

            return Ok("Usario dado de alta con éxito");
        }


        [HttpGet("Buscar")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult Buscar()
        {
            var usuarios = new List<Usuario>();
            try
            {
                usuarios = srvUsuario.Buscar();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al buscar usuarios: " + ex.Message);
            }

            return Ok(usuarios);
        }

        [HttpPut("Deshabilitar")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult Deshabilitar(UsuarioTransaccionModel model)
        {
            try
            {
                srvUsuario.Deshabilitar(model.Id);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al deshabilitar usuario: " + ex.Message);
            }

            return Ok("Usuario deshabilitado con éxito");
        }

        [HttpPut("ConvertirAdmin")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public ActionResult ConvertirAdmin(UsuarioTransaccionModel model)
        {
            try
            {
                srvUsuario.ConvertirAdmin(model.Id);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al convertir en admin el usuario: " + ex.Message);
            }

            return Ok("Usuario convertido en admin con éxito");
        }
    }
}

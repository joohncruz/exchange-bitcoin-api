using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [ActionName("Teste")]
        public IHttpActionResult PostTeste(string username, string password)
        {
            try
            {
                var response = Models.User.Buscar(username, password);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult PostUsuario([FromBody] User user)
        {
            try
            {
                user.Inserir();
                return Created("", user);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}

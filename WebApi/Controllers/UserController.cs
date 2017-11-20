using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get([FromBody] User user)
        {
            try
            {
                var response = Models.User.Buscar(user.UserName, user.Password);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] User user)
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

using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string username, string password)
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

using System.Web.Http;
using WebApi.Models;
using WebApi.Models.Requests;
using WebApi.Models.Requests.Validations;
using System.Linq;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class ExchangeController : ApiController
    {
        private IExchangeFactory _traderFactory;

        public ExchangeController()
        {
            _traderFactory = new ExchangeFactory();
        }

        [HttpPost, Authorize]
        [Route("calculator")]
        public IHttpActionResult PostCalcularValores([FromBody] CalcularValoresRequest payload,
                                                 [FromUri] int traderCompra,
                                                 [FromUri] int traderVenda)
        {
            if (payload == null)
                return BadRequest();

            var validator = new CalcularValoresRequestValidator();
            var validationResult = validator.Validate(payload);

            if (!validationResult.IsValid)
                return BadRequest(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));

            var calculator = new Calculator(
                _traderFactory.CreateTrader(traderCompra), 
                _traderFactory.CreateTrader(traderVenda));

            var result = calculator.CalcularValores(payload);
            return Ok(result);
        }

        [HttpPost, Authorize]
        [Route("comprar")]
        public IHttpActionResult PostComprar([FromBody] PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                return BadRequest();

            try
            {
                purchaseOrder.Inserir();
                return Created("",purchaseOrder);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet, Authorize]
        [Route("buscar")]
        public IHttpActionResult GetBuscar()
        {
            try
            {
                var list = PurchaseOrder.BuscarOrdensDeCompra(1);
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

using System.Web.Http;
using WebApi.Models;
using WebApi.Models.Requests;
using WebApi.Models.Requests.Validations;
using System.Linq;
using Newtonsoft.Json;

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

        [HttpPost]
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

        [HttpPost]
        [Route("comprar")]
        public IHttpActionResult PostComprar([FromBody] Buy buy)
        {
            if (buy == null)
                return BadRequest();

            var text = System.IO.File.ReadAllText(@"C:\Users\Public\MoedaVirtual\dados.txt");
            var newText = string.Concat(text, JsonConvert.SerializeObject(buy));

            System.IO.File.WriteAllText(@"C:\Users\Public\MoedaVirtual\dados.txt", string.Concat(text, JsonConvert.SerializeObject(buy), ","));

            return Ok(newText);
        }

    }
}

using ApplicationCore.Application.Services.Tests.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ApiController
    {
        [HttpGet("{pempresa}/{previsao}/{pveiculoversao}")]
        public async Task<ActionResult<FunctionVm>> Get(long pempresa, string previsao, string pveiculoversao)
        {
            FunctionVm data = await Mediator.Send(new GetDataFromFunctionQuery { pempresa = pempresa, previsao = previsao, pveiculoversao = pveiculoversao });

            return data;
        }
    }
}
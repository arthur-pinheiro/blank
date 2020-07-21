using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    public abstract class BaseResponse : BaseMessage
    {
        public BaseResponse(Guid correlationId) : base()
        {
            base._correlationId = correlationId;
        }

        public BaseResponse()
        {
        }
    }

    public class GetByIdClientResponse : BaseResponse
    {
        public GetByIdClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdClientResponse()
        {
        }

        public Client ClientTest { get; set; }
    }

    public abstract class BaseMessage
    {
        /// <summary>
        /// Unique Identifier used by logging
        /// </summary>
        protected Guid _correlationId = Guid.NewGuid();
        public Guid CorrelationId() => _correlationId;
    }

    public abstract class BaseRequest : BaseMessage
    {
    }

    public class GetByIdClientRequest : BaseRequest
    {
        public int ClientId { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAsyncRepository<Client> _clientRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAsyncRepository<Client> clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("testando-client/{ClientId}")]
        public async Task<ActionResult<GetByIdClientResponse>> HandleAsync([FromRoute] GetByIdClientRequest request)
        {
            var response = new GetByIdClientResponse(request.CorrelationId());

            var client = await _clientRepository
                .GetByIdAsync(request.ClientId);

            return NotFound();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using LoggingBestPract.Application.Features.Auth.Command.Login;
using LoggingBestPract.Application.Features.Auth.Command.Register;


namespace LoggingBestPract.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<AuthController> _logger;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            logger.Error("selam çukuulatam");
            _logger.LogError(1,"login controller");
            var response = await mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}

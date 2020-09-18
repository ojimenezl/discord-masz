using System;
using System.Threading.Tasks;
using masz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace masz.Controllers
{
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> logger;
        private readonly IOptions<InternalConfig> config;

        public IndexController(ILogger<IndexController> logger, IOptions<InternalConfig> config)
        {
            this.logger = logger;
            this.config = config;
        }

        [HttpGet("/")]
        public IActionResult Index() {
            logger.LogInformation("/ | Returning Index.");
            return new ContentResult() 
            {
                Content = "<a href=\"/api/v1/login\" target=\"blank\">Login with Discord.</a>",
                ContentType = "text/html",
            };
        }

        [HttpGet("/api/v1/status")]
        [HttpGet("/api/v1/health")]
        [HttpGet("/api/v1/healthcheck")]
        [HttpGet("/api/v1/ping")]
        public IActionResult Status() {
            logger.LogInformation("/status | Returning Status.");

            var accept = String.Empty;
            if (HttpContext.Request.Headers.ContainsKey("Accept")) {
                if (HttpContext.Request.Headers["Accept"].ToString().Contains("application/json"))
                {
                    return Ok(new {
                        status = "ok",
                        name = config.Value.ServiceHostName,
                        server_time = DateTime.Now.ToString(),
                        server_time_utc = DateTime.UtcNow.ToString()
                    });
                }
            }
            return Ok("OK");
        }
    }
}
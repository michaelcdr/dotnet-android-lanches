using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AndroidLanches.API.Controllers
{
    public class BaseControllerApi<T> : ControllerBase
    {
        protected const string BAD_REQUEST_MSG = "Não realizar essa operação, tente novamente mais tarde.";
        protected readonly ILogger<T> _logger;

        public BaseControllerApi(ILogger<T> logger) 
        {
            _logger = logger;
        }
    }
}

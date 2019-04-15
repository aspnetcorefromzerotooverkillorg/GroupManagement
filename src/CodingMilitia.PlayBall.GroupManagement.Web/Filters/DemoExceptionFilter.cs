using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Filters
{
    public class DemoExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<DemoExceptionFilter> _logger;

        public DemoExceptionFilter(ILogger<DemoExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException) {
                _logger.LogError("transforming argument exception in 400");
            }
        }
    }
}

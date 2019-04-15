using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Filters
{
    public class DemoActionFilter : IActionFilter
    {
        private readonly ILogger<DemoActionFilter> _logger;

        public DemoActionFilter(ILogger<DemoActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"after executed action {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"before executing action {context.ActionDescriptor.DisplayName}" +
                " with arguments {@arguments}", context.ActionArguments);
        }
    }
}

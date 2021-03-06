﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingMilitia.PlayBall.GroupManagement.Web.Middlewares
{
    public class RequestTimingAdHocMiddleware
    {
        private readonly RequestDelegate _next;
        private int _requestCounter;

        public RequestTimingAdHocMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, ILogger<RequestTimingAdHocMiddleware> logger)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();

            Interlocked.Increment(ref _requestCounter);
            //log
            logger.LogInformation($"\n request# {_requestCounter} took {watch.ElapsedMilliseconds}ms");
        }
    }
}

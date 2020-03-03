using CurrencyViewer.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CurrencyViewer.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode}";

        static readonly ILogger Log = Serilog.Log.ForContext<CustomExceptionFilterAttribute>();

        static readonly HashSet<string> HeaderWhitelist = new HashSet<string> { "Content-Type", "Content-Length", "User-Agent" };

        IWebHostEnvironment _hostingEnvironment;
        public CustomExceptionFilterAttribute(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            LogForErrorContext(context.HttpContext)
               .Error(context.Exception, MessageTemplate, context.HttpContext.Request.Method, GetPath(context.HttpContext), 500);

            var code = HttpStatusCode.InternalServerError;


            if (context.Exception is BadRequestException)
            {
                code = HttpStatusCode.NotFound;
            }

            if (context.Exception is InvalidParameterException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result =
                _hostingEnvironment.IsDevelopment()
                ?
                new JsonResult(new
                {
                    error = new[] { context.Exception.Message },
                    stackTrace = context.Exception.StackTrace
                })
                :
                new JsonResult(new
                {
                    error = new[] { context.Exception.Message }
                });
        }

        static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var loggedHeaders = request.Headers
                .Where(h => HeaderWhitelist.Contains(h.Key))
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            var result = Log
                .ForContext("RequestHeaders", loggedHeaders, destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            return result;
        }

        static string GetPath(HttpContext httpContext)
        {
            return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget
                ?? httpContext.Request.Path.ToString();
        }
    }
}

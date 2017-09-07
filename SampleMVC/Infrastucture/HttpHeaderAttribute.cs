using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleMVC.Infrastucture
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple = true)]
    public class HttpHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public HttpHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(
                _name, new string[] { _value });
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }

    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //Handle all exception thrown during action method execution
        }
    }
}
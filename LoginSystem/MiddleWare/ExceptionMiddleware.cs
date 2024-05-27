using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LoginSystem.MiddleWare
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}
		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
			string result = JsonConvert.SerializeObject(new ErrorDeatils
			{
				ErrorMessage = exception.Message,
				ErrorType = "Failure"
			});

			context.Response.StatusCode = (int)statusCode;
			return context.Response.WriteAsync(result);
		}
	}

	public class ErrorDeatils
	{
		public string ErrorType { get; set; }
		public string ErrorMessage { get; set; }
	}
}


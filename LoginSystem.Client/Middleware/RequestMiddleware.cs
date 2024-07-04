using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LoginSystem.Client.Service;

namespace LoginSystem.Client.Middleware
{
	public class RequestMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IServiceScopeFactory _scopeFactory;

		public RequestMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
		{
			_next = next;
			_scopeFactory = scopeFactory;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var _sessionService = scope.ServiceProvider.GetRequiredService<SessionService>();

				try
				{
					var ep = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
					var authAttr = ep?.Metadata?.GetMetadata<AuthorizeAttribute>();
					if (authAttr != null)
					{
						var tokenExists = _sessionService.TokenExists("token");
						var tokenIsValid = true;
						if (tokenExists)
						{
							var token = _sessionService.GetToken();
							JwtSecurityTokenHandler tokenHandler = new();
							var tokenContent = tokenHandler.ReadJwtToken(token);
							var expiry = tokenContent.ValidTo;
							if (expiry < DateTime.Now)
							{
								tokenIsValid = false;
							}
						}

						if (tokenIsValid == false || tokenExists == false)
						{
							await SignOutAndRedirect(httpContext);
							return;
						}
					}
					await _next(httpContext);
				}
				catch (Exception ex)
				{
					throw;
					//await HandleExceptionAsync(httpContext, ex);
				}
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			switch (exception)
			{
				default:
					var path = $"/Home/Error";
					context.Response.Redirect(path);
					break;
			}
		}

		private static async Task SignOutAndRedirect(HttpContext httpContext)
		{
			await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			var path = $"/Authenticate/Index";
			httpContext.Response.Redirect(path);
		}
	}
}

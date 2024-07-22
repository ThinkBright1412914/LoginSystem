using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class BaseController : ControllerBase
	{
	}
}

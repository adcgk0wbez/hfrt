using app.Domain.Interfaces;
using app.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController(ILogger<UserController> logger, IUserService userService) : ControllerBase
	{
		[HttpGet(Name = "GetUser")]
		public ResponseDto Get()
		{
			try
			{
				var usersData = userService.GetUsersDataAsync().Result;

				return usersData;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "error");
				throw;
			}
		}
	}
}

using app.Server.Models;
using app.Domain.Models;

namespace app.Domain.Interfaces
{
	public interface IUserService
	{
		Task<ResponseDto> GetUsersDataAsync();
	}
}

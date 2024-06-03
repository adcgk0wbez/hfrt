using app.Domain.Models;

namespace app.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<List<UserEntity>?> GetUsersAsync();
	}
}

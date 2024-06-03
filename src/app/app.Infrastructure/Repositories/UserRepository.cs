using app.Domain.Interfaces;
using app.Domain.Models;

namespace app.Infrastructure.Repositories
{
	public class UserRepository(IHighfieldClient highfieldClient) : IUserRepository
	{
		public async Task<List<UserEntity>?> GetUsersAsync()
		{
			var users = await highfieldClient.GetUsersAsync();

			return users;
		}
	}
}

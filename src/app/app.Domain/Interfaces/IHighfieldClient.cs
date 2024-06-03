using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.Domain.Models;
using app.Server.Models;

namespace app.Domain.Interfaces
{
	public interface IHighfieldClient
	{
		Task<List<UserEntity>?> GetUsersAsync();
	}
}

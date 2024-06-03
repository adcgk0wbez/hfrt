using app.Server.Models;

namespace app.Domain.Models
{
	public class ResponseDto
	{
		public List<UserEntity>? Users { get; set; }
		public List<AgePlusTwentyDto>? Ages { get; set; }
		public List<TopColoursDto>? TopColours { get; set; }
	}
}

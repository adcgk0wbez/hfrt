using System.Text.Json.Serialization;

namespace app.Domain.Models
{
	public class UserEntity
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("firstName")]
		public string? FirstName { get; set; }

		[JsonPropertyName("lastName")]
		public string? LastName { get; set; }

		[JsonPropertyName("email")]
		public string? Email { get; set; }

		[JsonPropertyName("dob")]
		public DateTime? Dob { get; set; }

		[JsonPropertyName("favouriteColour")]
		public string? FavouriteColour { get; set; }
	}
}

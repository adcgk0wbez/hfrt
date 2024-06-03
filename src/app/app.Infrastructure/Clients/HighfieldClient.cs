using System.Text.Json;
using app.Domain.Interfaces;
using app.Domain.Models;

namespace app.Infrastructure.Clients
{
	public class HighfieldClient(HttpClient httpClient) : IHighfieldClient
	{
		public async Task<List<UserEntity>?> GetUsersAsync()
		{
			var response = await httpClient.GetAsync("api/test");
			response.EnsureSuccessStatusCode();
			
			await using var responseStream = await response.Content.ReadAsStreamAsync();
			
			var userData = await JsonSerializer.DeserializeAsync<List<UserEntity>>(responseStream);
			
			return userData;

			// var jsonString = await File.ReadAllTextAsync("data.json");
			// var userData = JsonSerializer.Deserialize<List<UserEntity>>(jsonString);
			// return userData ?? new List<UserEntity>();
		}
	}
}

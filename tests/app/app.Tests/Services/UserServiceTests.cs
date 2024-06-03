using app.Application.Services;
using app.Domain.Interfaces;
using app.Domain.Models;
using app.Server.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace app.Tests.Services
{
	public class UserServiceTests
	{
		private readonly Mock<IUserRepository> _mockUserRepository = new();
		private readonly MemoryCache _mockMemoryCache = new(new MemoryCacheOptions());

		private UserService CreateUserService()
		{
			return new UserService(_mockUserRepository.Object, _mockMemoryCache);
		}

		[Fact]
		async Task GetUsersDataAsync_ReturnsExpectedResult_WhenUsersExist()
		{
			//arrange
			_mockUserRepository.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(UserData);

			var userService = CreateUserService();

			//act
			var result = await userService.GetUsersDataAsync();
	
			//assert
			Assert.NotNull(result);

			Assert.Equal(AgesPlusTwentyData.Count, result.Ages!.Count);
			for (var i = 0; i < AgesPlusTwentyData.Count; i++)
			{
				Assert.Equal(AgesPlusTwentyData[i].UserId, result.Ages[i].UserId);
				Assert.Equal(AgesPlusTwentyData[i].OriginalAge, result.Ages[i].OriginalAge);
				Assert.Equal(AgesPlusTwentyData[i].AgePlusTwenty, result.Ages[i].AgePlusTwenty);
			}

			Assert.Equal(TopColoursData.Count, result.TopColours!.Count);
			for (var i = 0; i < TopColoursData.Count; i++)
			{
				Assert.Equal(TopColoursData[i].Colour, result.TopColours[i].Colour);
				Assert.Equal(TopColoursData[i].Count, result.TopColours[i].Count);
			}
		}
	
		[Fact]
		async Task GetUsersDataAsync_ThrowsInvalidOperationException_WhenNoUsersExist()
		{
			//arrange
			_mockUserRepository.Setup(repo => repo.GetUsersAsync()).ReturnsAsync((List<UserEntity>)null!);

			var userService = CreateUserService();

			//act
			//assert
			await Assert.ThrowsAsync<InvalidOperationException>(() => userService.GetUsersDataAsync());
		}

		private static List<UserEntity> UserData => new() {
				new UserEntity
				{
					Id = Guid.Parse("000ba277-fd7d-477d-a22c-fb89a1824b9a"),
					FirstName = "Elie",
					LastName = "Tomkies",
					Email = "etomkiesfl@example.com",
					Dob = DateTime.Parse("1963-11-10T00:00:00"),
					FavouriteColour = "Red"
				},
				new UserEntity
				{
					Id = Guid.Parse("001469da-1d65-4557-8ffd-8810ff8c22f6"),
					FirstName = "Isacco",
					LastName = "Vedeneev",
					Email = "ivedeneev3j@boston.com",
					Dob = DateTime.Parse("2000-08-19T00:00:00"),
					FavouriteColour = "Red"
				},
				new UserEntity
				{
					Id = Guid.Parse("0046ad9c-ec76-4ff2-90d0-10fc8547fd70"),
					FirstName = "Massimiliano",
					LastName = "Gerkens",
					Email = "mgerkensm5@google.pl",
					Dob = DateTime.Parse("1976-07-25T00:00:00"),
					FavouriteColour = "Blue"
				},
				new UserEntity
				{
					Id = Guid.Parse("009bcaf3-6491-4be3-be81-56b1ae418f60"),
					FirstName = "Karola",
					LastName = "Yorston",
					Email = "kyorston6s@technorati.com",
					Dob = DateTime.Parse("1915-01-15T00:00:00"),
					FavouriteColour = "Blue"
				},
				new UserEntity
				{
					Id = Guid.Parse("00a7071b-3ade-4b44-9436-396d24a107a6"),
					FirstName = "Giacobo",
					LastName = "Fazzioli",
					Email = "gfazzioli94@statcounter.com",
					Dob = DateTime.Parse("1995-11-20T00:00:00"),
					FavouriteColour = "Green"
				},
				new UserEntity
				{
					Id = Guid.Parse("00bba2bd-c687-4f01-973f-09f319e1b1a2"),
					FirstName = "Marylynne",
					LastName = "Ambrosi",
					Email = "mambrosil7@blogger.com",
					Dob = DateTime.Parse("1965-08-24T00:00:00"),
					FavouriteColour = "Green"
				},
				new UserEntity
				{
					Id = Guid.Parse("00e78a9e-5a1d-437c-a40a-981623f61d24"),
					FirstName = "Sosanna",
					LastName = "Gerrels",
					Email = "sgerrels12@linkedin.com",
					Dob = DateTime.Parse("1906-11-19T00:00:00"),
					FavouriteColour = "Green"
				}
	};

		private static List<AgePlusTwentyDto> AgesPlusTwentyData =>
			new()
			{
				new AgePlusTwentyDto { UserId = Guid.Parse("000ba277-fd7d-477d-a22c-fb89a1824b9a"), OriginalAge = 60, AgePlusTwenty = 80 },
				new AgePlusTwentyDto { UserId = Guid.Parse("001469da-1d65-4557-8ffd-8810ff8c22f6"), OriginalAge = 23, AgePlusTwenty = 43 },
				new AgePlusTwentyDto { UserId = Guid.Parse("0046ad9c-ec76-4ff2-90d0-10fc8547fd70"), OriginalAge = 47, AgePlusTwenty = 67 },
				new AgePlusTwentyDto { UserId = Guid.Parse("009bcaf3-6491-4be3-be81-56b1ae418f60"), OriginalAge = 109, AgePlusTwenty = 129 },
				new AgePlusTwentyDto { UserId = Guid.Parse("00a7071b-3ade-4b44-9436-396d24a107a6"), OriginalAge = 28, AgePlusTwenty = 48 },
				new AgePlusTwentyDto { UserId = Guid.Parse("00bba2bd-c687-4f01-973f-09f319e1b1a2"), OriginalAge = 58, AgePlusTwenty = 78 },
				new AgePlusTwentyDto { UserId = Guid.Parse("00e78a9e-5a1d-437c-a40a-981623f61d24"), OriginalAge = 117, AgePlusTwenty = 137 }
			};

		private static List<TopColoursDto> TopColoursData =>
			new()
			{
				new TopColoursDto { Colour = "Green", Count = 3 },
				new TopColoursDto { Colour = "Blue", Count = 2 },
				new TopColoursDto { Colour = "Red", Count = 2 }
			};
	}
}

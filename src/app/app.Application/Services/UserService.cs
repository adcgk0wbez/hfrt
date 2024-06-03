using app.Domain.Interfaces;
using app.Domain.Models;
using app.Server.Models;

namespace app.Application.Services
{
	public class UserService(IUserRepository userRepository) : IUserService
	{
		public async Task<ResponseDto> GetUsersDataAsync()
		{
			var users = await userRepository.GetUsersAsync();

			if (users == null)
			{
				throw new InvalidOperationException();
			}

			return new ResponseDto
			{
				Users = users,
				TopColours = CalculateColourFrequency(users),
				Ages = CalculateAgesPlusTwenty(users)
			};
		}

		//Calculate the frequency of each favorite colour in the list of users
		private List<TopColoursDto> CalculateColourFrequency(IEnumerable<UserEntity> users)
		{
			//If the users list is null - throw an exception
			return (users ?? throw new ArgumentNullException(nameof(users)))
				//Group the users by their favorite colour
				.GroupBy(user => user.FavouriteColour)
				//Create a new TopColoursDto for each group
				.Select(group => new TopColoursDto
				{
					//Set the colour to the group key / favorite colour
					//and count to the number of users in the group
					Colour = group.Key,
					Count = group.Count()
				})
				//Sort by count decending
				.OrderByDescending(dto => dto.Count)
				//Then (or, if two colours have the same count) sort alphabetically on colour
				.ThenBy(dto => dto.Colour)
				.ToList();
		}

		private List<AgePlusTwentyDto> CalculateAgesPlusTwenty(IEnumerable<UserEntity> users)
		{
			//If the users list is null - throw an exception
			return (users ?? throw new ArgumentNullException(nameof(users))).Select(user =>
			{
				if (!user.Dob.HasValue)
				{
					throw new InvalidOperationException("dob dfgsagsdf " + user.Id);
				}

				//Get the current year minus the year of the user's date of birth
				var currentYearMinusDobYear = DateTime.Today.Year - user.Dob.Value.Year;

				//Check if current day of the year (1-365) -
				//is greater than or equal to the day the year of the user's date of birth
				var birthdayOccurredThisYear = DateTime.Today.DayOfYear >= user.Dob.Value.DayOfYear;

				//If the user's birthday has not occurred this year - subtract 1 from currentYearMinusDobYear
				var age = birthdayOccurredThisYear ? currentYearMinusDobYear : currentYearMinusDobYear - 1;

				return new AgePlusTwentyDto
				{
					UserId = user.Id,
					OriginalAge = age,
					AgePlusTwenty = age + 20
				};
			}).ToList();
		}
	}
}

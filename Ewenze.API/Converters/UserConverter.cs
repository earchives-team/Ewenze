using Ewenze.API.Models.UsersDto;
using Ewenze.Application.Services.Users.Models;

namespace Ewenze.API.Converters
{
    public class UserConverter
    {
        public IList<UserOutputDto> Convert(IEnumerable<UserApplicationModel> users) => users?.Select(Convert).ToList(); 

        public UserOutputDto Convert(UserApplicationModel user)
        {
            if (user == null) return null;

            return new UserOutputDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            }; 
        }

        public UserApplicationModel Convert(UserInputDto userInputDto)
        {
            return new UserApplicationModel
            {
                Id = userInputDto.Id,
                Email = userInputDto.Email,
                Name = userInputDto.Name,
                PhoneNumber = userInputDto.PhoneNumber,
                Password = userInputDto.Password,
                BirthDate =  new DateTime(userInputDto.BirthDate.Year, userInputDto.BirthDate.Month, userInputDto.BirthDate.Day).Date,
            };
        }
    }
}

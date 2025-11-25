using Ewenze.API.Models.UsersDto;
using Ewenze.Application.Services.Users.Models;

namespace Ewenze.API.Converters
{
    public class UserConverter
    {
        public IList<UserOutputDto> Convert(IEnumerable<User> users) => users?.Select(Convert).ToList(); 

        public UserOutputDto Convert(User user)
        {
            if (user == null) return null;

            return new UserOutputDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            }; 
        }

        public User Convert(UserInputDto userInputDto)
        {
            return new User
            {
                Id = userInputDto.Id,
                Email = userInputDto.Email,
                Name = userInputDto.Name,
                PhoneNumber = userInputDto.PhoneNumber,
                Password = userInputDto.Password
            };
        }
    }
}

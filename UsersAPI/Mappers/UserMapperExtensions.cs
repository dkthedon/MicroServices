using DataContracts;
using UsersAPI.Entities;

namespace UsersAPI.Mappers
{
    public static class UserMapperExtensions
    {
        public static User Map(this UserDto userDto)
        {
            return new User
            {
                Age = userDto.Age,
                Name = userDto.Name,
                Email = userDto.Email
            };
        }

        public static UserDto Map(this User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Age = user.Age,
                Email = user.Email
            };
        }

        public static UserNameDto MapName(this User user)
        {
            return new UserNameDto
            {
                UserId = user.UserId,
                Name = user.Name
            };
        }
    }
}

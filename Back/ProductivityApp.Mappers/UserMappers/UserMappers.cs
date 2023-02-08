using ProductivityApp.Domain.Entities;
using ProductivityApp.Dtos.UserDtos;
using ProductivityApp.Mappers.UserSessionMappers;

namespace ProductivityApp.Mappers.UserMappers
{
    public static class UserMappers
    {


        public static UserDto ToUserDto(this User userDb)
        {
            return new UserDto
            {
                FullName = userDb.Fullname,
                UserName = userDb.Username,
                Role = userDb.Role,
                UserSessions = userDb.UserSessions.Select(s => s.ToUserSessionDto()).ToList()
            };
        }
        public static User ToUserDb (this RegisterUserDto registerUserDto)
        {
            return new User
            {
                Email = registerUserDto.Email,
                Username = registerUserDto.Username,
                Fullname = registerUserDto.Fullname,
               
            
                
            };
        }
        public static User ResetPasswordMapper(this ResetPasswordDto request, User userDb)
        {
            userDb.PasswordResetToken = request.Token;
          //  userDb.Password = request.Password;

            return userDb;
        }
        //public static UserSession UpdateDbUserSession(this UpdateUserSessionDto updateUserSessionDto, UserSession UserSessionDb)
        //{
        //    UserSessionDb.StartTime = updateUserSessionDto.StartTime;
        //    UserSessionDb.FinishTime = updateUserSessionDto.FinishTime;
        //    UserSessionDb.UserSessionLength = updateUserSessionDto.UserSessionLength;
        //    UserSessionDb.Tasks = updateUserSessionDto.Tasks.Select(t => t.ToTask()).ToList();
        //    UserSessionDb.UserId = updateUserSessionDto.UserId;

        //    return UserSessionDb;

        //}
    }
}

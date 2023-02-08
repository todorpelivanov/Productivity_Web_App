using ProductivityApp.Domain.Entities;
using ProductivityApp.Dtos.UserSessionDtos;
using ProductivityApp.Mappers.TaskMappers;

namespace ProductivityApp.Mappers.UserSessionMappers
{
    public static class UserSessionMapper
    {

        public static UserSessionDto ToUserSessionDto(this UserSession UserSessionDb)
        {
            return new UserSessionDto
            {
                StartTime = UserSessionDb.StartTime,
                FinishTime = UserSessionDb.FinishTime,
                UserSessionLength = UserSessionDb.UserSessionLength,
                Tasks = UserSessionDb.Tasks.Select(t => t.ToTaskDto()).ToList()
            };
        }


        public static UserSession ToUserSession(this AddUserSessionDto addUserSessionDto)
        {
            return new UserSession
            {
                StartTime = addUserSessionDto.StartTime,
                FinishTime = addUserSessionDto.FinishTime,
                UserSessionLength = addUserSessionDto.UserSessionLength,
                Tasks = addUserSessionDto.Tasks.Select(t => t.ToTask()).ToList(),
                UserId = addUserSessionDto.UserId,
            };
        }

        public static UserSession UpdateDbUserSession(this UpdateUserSessionDto updateUserSessionDto, UserSession UserSessionDb)
        {
            UserSessionDb.StartTime = updateUserSessionDto.StartTime;
            UserSessionDb.FinishTime = updateUserSessionDto.FinishTime;
            UserSessionDb.UserSessionLength = updateUserSessionDto.UserSessionLength;
            UserSessionDb.Tasks = updateUserSessionDto.Tasks.Select(t => t.ToTask()).ToList();
            UserSessionDb.UserId = updateUserSessionDto.UserId;

            return UserSessionDb;

        }
    }
}

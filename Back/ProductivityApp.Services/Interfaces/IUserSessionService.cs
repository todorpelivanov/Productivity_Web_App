using ProductivityApp.Dtos.UserSessionDtos;

namespace ProductivityApp.Services.Interfaces
{
    public interface IUserSessionService
    {

        Task<List<UserSessionDto>> GetAllUserSessions(int userId);

        Task<UserSessionDto> GetUserSessionById(int id);

        Task AddUserSession(AddUserSessionDto addUserSessionDto, int userId);

        Task UpdateUserSession(UpdateUserSessionDto updateUserSessionDto);

        Task DeleteUserSession(int id);
    }
}

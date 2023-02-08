using ProducitivityApp.DataAccess.Interfaces;
using ProductivityApp.Domain.Entities;
using ProductivityApp.Dtos.UserSessionDtos;
using ProductivityApp.Mappers.UserSessionMappers;
using ProductivityApp.Services.Interfaces;
using ProductivityApp.Shared.CustomUserSessionExceptions;
using ProductivityApp.Shared.CustomUserExceptions;
using Task = System.Threading.Tasks.Task;


namespace ProductivityApp.Services.Implementations
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _UserSessionRepository;
        private readonly IUserRepository _userRepository;
        

        public UserSessionService(IUserSessionRepository UserSessionRepository, IUserRepository userRepository)
        {
            _UserSessionRepository = UserSessionRepository;
            _userRepository = userRepository;
            
        }
        public async Task AddUserSession(AddUserSessionDto addUserSessionDto, int userId)
        {

            User userDb = await _userRepository.GetById(userId);
            if (userDb == null)
            {
                throw new UserNotFoundException($"User with id {addUserSessionDto.UserId} was not found in the database.");
            }
            if (string.IsNullOrEmpty(addUserSessionDto.StartTime.ToString("o")) || string.IsNullOrEmpty(addUserSessionDto.FinishTime.ToString("o")))
            {
                throw new UserSessionDataException("Start time and Finish time are required fields. ");
            }

            if (addUserSessionDto.UserSessionLength == 0)
            {
                throw new UserSessionDataException("UserSession length can not be zero.");
            }
            if (addUserSessionDto.Tasks.Count == 0)
            {
                throw new UserSessionDataException("The list of tasks for the current UserSession can not be empty.");
            }

            UserSession newUserSession = addUserSessionDto.ToUserSession();
            newUserSession.UserId = userId;

            await _UserSessionRepository.Add(newUserSession);
        }

        public async Task DeleteUserSession(int id)
        {
            UserSession UserSessionDb = await _UserSessionRepository.GetById(id);
            if (UserSessionDb == null)
            {
                throw new UserSessionNotFoundException($"UserSession with id {id} was not found in the database.");
            }
            await _UserSessionRepository.Delete(UserSessionDb);
        }

        public async Task<List<UserSessionDto>> GetAllUserSessions(int userId)
        {
            List<UserSession> UserSessionsDb = await _UserSessionRepository.GetAll();

            List<UserSessionDto> UserSessionsDto = UserSessionsDb.Where(x => x.UserId == userId).Select(s => s.ToUserSessionDto()).ToList();
            return UserSessionsDto;

        }

        public async Task<UserSessionDto> GetUserSessionById(int id)
        {
            UserSession UserSessionDb = await _UserSessionRepository.GetById(id);
            if (UserSessionDb == null)
            {
                throw new UserSessionNotFoundException($"UserSession with id {id} was not found in the database.");
            }
            UserSessionDto UserSessionDto = UserSessionDb.ToUserSessionDto();

            return UserSessionDto;
        }

        public async Task UpdateUserSession(UpdateUserSessionDto updateUserSessionDto)
        {
            UserSession UserSessionDb = await _UserSessionRepository.GetById(updateUserSessionDto.Id);
            if (UserSessionDb == null)
            {
                throw new UserSessionNotFoundException($"UserSession with id {updateUserSessionDto.Id} was not found in the database.");
            }

            User userDb = await _userRepository.GetById(updateUserSessionDto.UserId);
            if (userDb == null)
            {
                throw new UserNotFoundException($"User with id {updateUserSessionDto.UserId} was not found in the database.");
            }
            if (string.IsNullOrEmpty(updateUserSessionDto.StartTime.ToString("o")) || string.IsNullOrEmpty(updateUserSessionDto.FinishTime.ToString("o")))
            {
                throw new UserSessionDataException("Start time and Finish time are required fields. ");
            }

            if (updateUserSessionDto.UserSessionLength == 0)
            {
                throw new UserSessionDataException("UserSession length can not be zero.");
            }
            if (updateUserSessionDto.Tasks.Count == 0)
            {
                throw new UserSessionDataException("The list of tasks for the current UserSession can not be empty.");
            }

            updateUserSessionDto.UpdateDbUserSession(UserSessionDb);

            await _UserSessionRepository.Update(UserSessionDb);
        }
    }
}

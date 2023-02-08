using ProductivityApp.Domain.Entities;
using ProductivityApp.Domain.Enums;
using ProductivityApp.Dtos.UserSessionDtos;

namespace ProductivityApp.Dtos.UserDtos
{
    public  class UserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public RoleEnum Role { get; set; } = RoleEnum.User;
        public List<UserSessionDto> UserSessions { get; set; } = new List<UserSessionDto>() { };
    }
}

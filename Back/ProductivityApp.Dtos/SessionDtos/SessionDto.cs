using ProductivityApp.Dtos.TaskDtos;

namespace ProductivityApp.Dtos.UserSessionDtos
{
    public  class UserSessionDto
    {

        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset FinishTime { get; set; } = DateTimeOffset.UtcNow;

        public int UserSessionLength { get; set; } = 0;

        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>() { };
    }
}

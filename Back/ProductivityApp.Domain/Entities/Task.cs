using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductivityApp.Domain.Enums;
using System.Reflection.Metadata.Ecma335;

namespace ProductivityApp.Domain.Entities
{
    public  class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public int AssignedTimeDuration { get; set; } = 0;

        public string Note { get; set; } = string.Empty;
        public PriorityEnum Priority { get; set; } = PriorityEnum.Low;
        public PaceEnum Pace { get; set; } = PaceEnum.Low;

        public UserSession? UserSession { get; set; }
        public int UserSessionId { get; set; }

    }
}

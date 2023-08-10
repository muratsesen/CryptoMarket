using Domain;
using Domain.Enums;
namespace Application.Models
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int InstructionId { get; set; }
        public ChannelType ChannelType { get; set; }
        public DateTime? SentDateTime { get; set; }
    }
}

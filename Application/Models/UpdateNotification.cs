using Domain.Enums;

namespace Application.Models
{
    public class UpdateNotification
    {
        public int Id { get; set; }
        public ChannelType ChannelType { get; set; }
        public DateTime? SentDateTime { get; set; }
    }
}

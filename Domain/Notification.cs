using Domain.Enums;
namespace Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public int InstructionId { get; set; }
        public ChannelType ChannelType { get; set; }
        public DateTime? SentDateTime { get; set; }
        public Instruction Instruction { get; set; }
    }
}

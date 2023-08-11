using Domain.Enums;
public class SendNotificaitonDto
{
    public int Id { get; set; }
    public string Message { get; set; } = "";
    public ChannelType ChannelType { get; set; }
    public int InstructionId { get; set; }
    public string UserName { get; set; }

}
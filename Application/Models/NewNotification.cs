using Domain;
using Domain.Enums;

namespace Application.Models;
public class NewNotification
{
    public int Id { get; set; }
    public int InstructionId { get; set; }
    public ChannelType ChannelType { get; set; }
}
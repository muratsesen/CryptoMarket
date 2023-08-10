using Domain;
namespace Application.Models;
public class NewInstruction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int DayOfMonth { get; set; }
    public int UserId { get; set; }
    public List<NewNotification> Notificaitons { get; set; }
}
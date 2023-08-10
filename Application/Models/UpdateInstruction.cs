namespace Application.Models
{
    public class UpdateInstruction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int DayOfMonth { get; set; }
        public bool IsDone { get; set; } = false;

    }
}

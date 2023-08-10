namespace Application.Models
{
    public class InstructionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int DayOfMonth { get; set; }
        public bool IsDone { get; set; } = false;
        public int UserId { get; set; }
        public UserDto User { get; set; }

    }
}

namespace Domain
{
    public class Instruction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int DayOfMonth { get; set; }
        public bool IsDone { get; set; } = false;
        public int UserId { get; set; } 
        public User User { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}

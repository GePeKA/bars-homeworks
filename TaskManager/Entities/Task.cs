namespace TaskManager.Entities
{
    public class Task
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
    }
}

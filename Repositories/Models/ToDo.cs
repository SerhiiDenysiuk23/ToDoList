namespace Repositories.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

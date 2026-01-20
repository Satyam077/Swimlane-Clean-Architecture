namespace Swimlane.Core.DTOs
{
    public class TestResponseDto
    {
        public Guid Id { get; set; }
        public string TestName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

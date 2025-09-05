namespace Domain.DTOs
{
    public record ToolUpdateDTO
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool IsAvailable { get; init; }
        public int ToolTypeId { get; init; }
    }
}
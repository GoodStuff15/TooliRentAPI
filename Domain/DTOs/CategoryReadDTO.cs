namespace Domain.DTOs
{
    public record CategoryReadDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal DelayPrice { get; init; }
        // Add other relevant properties as needed
    }
}
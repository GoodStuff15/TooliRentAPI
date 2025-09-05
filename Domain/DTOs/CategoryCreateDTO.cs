namespace Domain.DTOs
{
    public record CategoryCreateDTO
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal DelayPrice { get; init; }
    }
}
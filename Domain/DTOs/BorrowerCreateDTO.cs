namespace Domain.DTOs
{
    public record BorrowerCreateDTO
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public bool IsActive { get; init; } 
    }
}
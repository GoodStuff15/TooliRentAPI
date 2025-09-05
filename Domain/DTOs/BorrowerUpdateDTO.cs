namespace Domain.DTOs
{
    public record BorrowerUpdateDTO
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }
}
namespace Domain.DTOs
{
    public record BorrowerCreateDTO
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;  

        public string PhoneNumber { get; init; } = string.Empty;

        public string Address { get; init; } = string.Empty;
        public bool IsActive { get; init; } 
    }
}
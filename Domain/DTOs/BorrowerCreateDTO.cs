namespace Domain.DTOs
{
    public record BorrowerCreateDTO
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;  

        public string PhoneNumber { get; init; } = string.Empty;

        public string Address { get; init; } = string.Empty;

        public string UserId { get; init; } = string.Empty; // Foreign key to User 
        public bool IsActive { get; init; } 
    }
}
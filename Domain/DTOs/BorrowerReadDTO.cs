namespace Domain.DTOs
{
    public record BorrowerReadDTO
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public bool IsActive { get; init; }

        public IEnumerable<BookingReadDTO> Bookings { get; init; } = new List<BookingReadDTO>();
    }
}
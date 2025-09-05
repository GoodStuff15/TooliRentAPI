namespace Domain.DTOs
{
    public record BookingReadDTO
    {
        public int Id { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int BorrowerId { get; init; }
        public IEnumerable<ToolReadDTO> Tools { get; init; } = Enumerable.Empty<ToolReadDTO>();

        // Status

        public DateOnly PickedUpDate { get; init; }

        public DateOnly ReturnedDate { get; init; }

        public bool IsActive { get; init; }

        public bool WasPickedUp { get; init; }

        public bool WasReturned { get; init; }

        public bool IsCompleted { get; init; }

        public bool IsCancelled {  get; init; }
    }
}
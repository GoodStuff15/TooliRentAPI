namespace Domain.DTOs
{
    public record BookingCreateDTO
    {
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public int BorrowerId { get; init; }
        public bool IsActive { get; init; }

        public DateOnly? PickedUpDate { get; init; }
       
        public IEnumerable<int> ToolIds { get; init; } = Enumerable.Empty<int>();
    }
}
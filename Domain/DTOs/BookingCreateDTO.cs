namespace Domain.DTOs
{
    public record BookingCreateDTO
    {
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public int BorrowerId { get; init; }
        public bool IsActive { get; init; }

        public bool PickedUp { get; init; } = false;
       
        public IEnumerable<int> ToolIds { get; init; } = Enumerable.Empty<int>();
    }
}
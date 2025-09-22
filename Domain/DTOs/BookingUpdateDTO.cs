namespace Domain.DTOs
{
    public record BookingUpdateDTO
    {

        public int BookingId { get; set; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }

        public DateOnly? PickedUpDate { get; init; } 

        public DateOnly? ReturnedDate { get; init; }

        public bool? IsDeleted { get; init; }
        public bool? IsCompleted { get; init; }
        public bool? WasReturned { get; init; }
        public bool? IsActive { get; init; }

        public bool? IsCancelled { get; init; }

        public IEnumerable<int> ToolIds { get; init; } = Enumerable.Empty<int>();

    }
}
namespace Domain.DTOs
{
    public record BookingUpdateDTO
    {
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }

        public DateTime? PickedUpDate { get; init; } 

        public DateTime? ReturnedDate { get; init; }

        public bool? IsDeleted { get; init; }
        public bool? IsCompleted { get; init; }
        public bool? WasReturned { get; init; }
        public bool? IsActive { get; init; }

        public bool? IsCancelled { get; init; }
        
    }
}
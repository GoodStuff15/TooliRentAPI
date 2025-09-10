using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet] // Does this work? Just IActionResult?
        public async Task<IActionResult> GetAllBookings(CancellationToken ct = default)
        {
            var bookings = await _bookingService.GetAllBookings(ct);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id, CancellationToken ct = default)
        {
            var booking = await _bookingService.GetBookingById(id, ct);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(int userId, CancellationToken ct = default)
        {
            var bookings = await _bookingService.GetAllUserBookingsAsync(userId, ct);
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var receipt = await _bookingService.CreateBooking(dto, ct);
            return CreatedAtAction(nameof(GetBookingById), new { id = receipt.BookingId }, receipt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateDTO dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookingService.UpdateBooking(id, dto, ct);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id, CancellationToken ct = default)
        {
            var result = await _bookingService.DeleteBooking(id, ct);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> CompleteBooking(int id, CancellationToken ct = default)
        {
            var result = await _bookingService.CompleteBooking(id, ct);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Booking returned");
        }

        [HttpPut("extend/{id}")]
        public async Task<IActionResult> ExtendBooking(int id, [FromQuery] DateOnly newEndDate, CancellationToken ct = default)
        {
            var result = await _bookingService.ExtendBooking(id, newEndDate, ct);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Booking extended");
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id, CancellationToken ct = default)
        {
            var result = await _bookingService.CancelBooking(id, ct);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Booking canceled");
        }

        [HttpPut("pickup/{id}")]
        public async Task<IActionResult> PickupBooking(int id, CancellationToken ct = default)
        {
            var result = await _bookingService.PickupBooking(id, ct);
            if (!result)
            {
                return NotFound();
            }
            return Ok("Booking picked up");
        }
    }
}

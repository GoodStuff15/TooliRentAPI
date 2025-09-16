using Application.Services;
using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<BookingCreateDTO> _newBookingValidator;

        public BookingController(IBookingService bookingService, IValidator<BookingCreateDTO> createValidator)
        {
            _bookingService = bookingService;
            _newBookingValidator = createValidator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<BookingReadDTO>>> GetAllBookings(CancellationToken ct = default)
        {
            var bookings = await _bookingService.GetAllBookings(ct);
            return Ok(bookings);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingReadDTO>> GetBookingById(int id, CancellationToken ct = default)
        {
            var booking = await _bookingService.GetBookingById(id, ct);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("late/{late}")]
        public async Task<ActionResult<IEnumerable<BookingReadDTO>>> GetLateBookings(bool late, CancellationToken ct = default)
        {
            var bookings = await _bookingService.GetLateBookings(late, ct);
            return Ok(bookings);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingReadDTO>>> GetBookingsByUserId(int userId, CancellationToken ct = default)
        {
            var bookings = await _bookingService.GetAllUserBookingsAsync(userId, ct);
            return Ok(bookings);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult<BookingCreate_ResponseDTO>> CreateBooking([FromBody] BookingCreateDTO dto, CancellationToken ct = default)
        {
            var validationResult = await _newBookingValidator.ValidateAsync(dto, ct);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var response = await _bookingService.CreateBooking(dto, ct);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User")]
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

        [HttpPut("late/update")]
        public async Task<IActionResult> UpdateLateBookings(CancellationToken ct = default)
        {
            await _bookingService.UpdateLateBookings(ct);

            return Ok("Late bookings updated");
        }

        [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin")]
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

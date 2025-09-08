using AutoMapper;
using Domain.DTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public Task<BookingReceiptDTO> CreateBooking(BookingCreateDTO dto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBooking(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<BookingReadDTO>> GetAllBookings(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<BookingReadDTO?> GetBookingById(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBooking(int id, BookingUpdateDTO dto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        // NON-CRUD OPERATIONS

        public Task<bool> CancelBooking(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CompleteBooking(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExtendBooking(int id, DateTime newEndDate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}

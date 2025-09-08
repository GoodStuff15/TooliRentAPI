using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
     public class BookingMapConfig : Profile
    {
        public BookingMapConfig()
        {
            CreateMap<Booking, BookingReadDTO>();
            
            CreateMap<BookingCreateDTO, Booking>();
            
            CreateMap<BookingUpdateDTO, Booking>();

            CreateMap<Booking, BookingReceiptDTO>();
        }
    }
}

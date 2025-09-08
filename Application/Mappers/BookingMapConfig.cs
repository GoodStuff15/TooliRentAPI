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
            CreateMap<Booking, BookingReceiptDTO>()
                    .ForMember(dest => dest.BorrowedTools, opt => opt.Ignore())
                     .AfterMap( (src, dest, rc) =>
                     {
                        foreach (var tool in src.Tools)
                        {
                            dest.BorrowedTools.Add(rc.Mapper.Map<ToolReadDTO>(tool));
                        }
                     }
                );
            
            CreateMap<BookingCreateDTO, Booking>();
            
            CreateMap<BookingUpdateDTO, Booking>();

            CreateMap<Booking, BookingReadDTO>();
        }
    }
}

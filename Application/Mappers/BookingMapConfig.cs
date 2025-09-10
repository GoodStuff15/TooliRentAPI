using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.Data.SqlClient;
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
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest => dest.BorrowedTools, opt => opt.Ignore())
                     .AfterMap((src, dest, rc) =>
                     {
                         foreach (var tool in src.Tools)
                         {
                             dest.BorrowedTools.Add(rc.Mapper.Map<ToolReadDTO>(tool));
                         }
                     }
                        );

            CreateMap<BookingCreateDTO, Booking>()
                     .ForMember(dest => dest.PickedUpDate,
                      opt => opt.Ignore())
                     .AfterMap((src, dest, rc) =>
                     {
                         if(src.PickedUp)
                         {
                             dest.PickedUpDate = DateOnly.FromDateTime(DateTime.Now);
                             dest.WasPickedUp = true;
                         }
                     }
                        );

            CreateMap<BookingUpdateDTO, Booking>();

            CreateMap<Booking, BookingReadDTO>();
        }
    }
}

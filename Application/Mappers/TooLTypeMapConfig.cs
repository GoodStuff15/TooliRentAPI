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
    public class TooLTypeMapConfig : Profile
    {

        public TooLTypeMapConfig() 
        {

            CreateMap<ToolTypeCreateDTO, ToolType>();
            CreateMap<ToolTypeUpdateDTO, ToolType>();

            CreateMap<ToolType, ToolTypeReadDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));


        }
    }
}

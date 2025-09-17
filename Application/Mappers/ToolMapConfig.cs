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
    public class ToolMapConfig : Profile

    {

        public ToolMapConfig()
        {

            CreateMap<Tool, ToolReadDTO>()
                .ForMember(dest => dest.ToolTypeName,
                opt => opt.MapFrom(src => src.ToolType.Name))
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.ToolType.Category.Name));
            CreateMap<ToolUpdateDTO, Tool>();
            CreateMap<ToolCreateDTO, Tool>();
            CreateMap<Tool, ToolReadShorthandDTO>()
                .ForMember(dest => dest.TypeName,
                opt => opt.MapFrom(src => src.ToolType.Name))
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.ToolType.Category.Name));
        }
    }
}

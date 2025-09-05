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

            CreateMap<Tool, ToolReadDTO>();
            CreateMap<ToolUpdateDTO, Tool>();
            CreateMap<ToolCreateDTO, Tool>();
        }
    }
}

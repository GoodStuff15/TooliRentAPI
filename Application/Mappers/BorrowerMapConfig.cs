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
    public class BorrowerMapConfig : Profile
    {
        public BorrowerMapConfig()
        {

            CreateMap<Borrower, BorrowerReadDTO>();

            CreateMap<BorrowerCreateDTO, Borrower>();
            CreateMap<BorrowerUpdateDTO, Borrower>();
        }
    }
}

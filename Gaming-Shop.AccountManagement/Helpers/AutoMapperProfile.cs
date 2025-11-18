using AutoMapper;
using Gaming_Shop.AccountManagement.DTOs;
using Gaming_Shop.AccountManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Shop.AccountManagement.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO, User>();
        }
    }
}

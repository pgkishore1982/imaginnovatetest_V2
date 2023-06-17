using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using imaginnovatetest.Models;
using imaginnovatetest.Models.DTO;

namespace imaginnovatetest.MappingConfigurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<employeeDto,TblEmployee> ();
        }
    }
}

using AutoMapper;
using GenxAPI.Model;
using GenxAPI.Model.Dtos;
using GenxAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenxAPI.GenxMapper
{
    public class GenxMappings:Profile
    {
        public GenxMappings()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();          
        }
    }
}

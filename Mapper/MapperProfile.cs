using AutoMapper;
using Data.Models;
using Dto;

namespace Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Class, ClassDto>()
                .ReverseMap();

            CreateMap<Student, StudentDto>() 
                .ReverseMap();
        }
    }
}

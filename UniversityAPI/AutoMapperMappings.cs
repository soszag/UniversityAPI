using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Dto;
using UniversityAPI.Dto.CreationDto;
using UniversityAPI.Dto.UpdateDto;
using UniversityAPI.Models;

namespace UniversityAPI
{
    public class AutoMapperMappings
    {
        public static void AutoMapperConfiguration()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Teachers, TeacherDto>();
                cfg.CreateMap<Students, StudentDto>();
                cfg.CreateMap<Classes, ClassDto>();
                cfg.CreateMap<Classes, ClassSimpleDto>();
                cfg.CreateMap<Parents, ParentDto>();
                cfg.CreateMap<ParentDto, Parents>();
                cfg.CreateMap<ParentCreationDto, Parents>();
                cfg.CreateMap<ParentUpdateDto, Parents>();

                cfg.CreateMap<StudentCreationDto, Students>();

                cfg.CreateMap<ClassCreationDto, Classes>();
                cfg.CreateMap<Classes, ClassCreationDto>();

                cfg.CreateMap<UserCreationDto, Users>();
                cfg.CreateMap<UserCreationDto, Students>();
                cfg.CreateMap<UserCreationDto, Teachers>();
                cfg.CreateMap<UserCreationDto, Parents>();
                cfg.CreateMap<Users, UserCreationDto>();

                cfg.CreateMap<StudentUpdateDto, Students>();
                cfg.CreateMap<ClassUpdateDto, Classes>();
                cfg.CreateMap<TeacherUpdateDto, Teachers>();
                cfg.CreateMap<Teachers, TeacherCreationDto>();
                cfg.CreateMap<TeacherCreationDto, Teachers>();

                cfg.CreateMap<UserDto, Users>();
                cfg.CreateMap<Users, UserDto>();

                cfg.CreateMap<Students, UserDto>();
                cfg.CreateMap<Teachers, UserDto>();
                cfg.CreateMap<Parents, UserDto>();

                cfg.CreateMap<SubjectCreationDto, Subjects>();
                cfg.CreateMap<SubjectUpdateDto, Subjects>();
                cfg.CreateMap<SubjectDto, Subjects>();

                cfg.CreateMap<Claims, Claim>().
                        ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.ClaimName)).
                        ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.ClaimParameters));
            });
        }
    }
}

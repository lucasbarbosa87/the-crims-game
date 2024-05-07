using AutoMapper;
using Crims.Data.Dtos;
using Crims.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RegisterDto, UserEntity>();
            CreateMap<UserEntity, RegisterDto>().ForMember(dest => dest.Password, options => options.Ignore());
            CreateMap<UserEntity, UserDto>();

            CreateMap<EstablishmentEntity, EstablishmentDto>();
            CreateMap<EstablishmentDto, EstablishmentEntity>();
        }
    }
}

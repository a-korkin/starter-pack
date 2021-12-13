using System;
using AutoMapper;

namespace Application.Common.Profiles.Admin
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<Models.DTO.Admin.UserRoleInDto, Domain.Entities.Admin.UserRole>();
        }
    }
}
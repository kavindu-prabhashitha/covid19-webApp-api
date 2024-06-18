using AutoMapper;
using covid19_api.Dtos.Case;
using covid19_api.Dtos.CountryData;
using covid19_api.Dtos.RolePermission;
using covid19_api.Dtos.User;
using covid19_api.Dtos.UserRole;

namespace covid19_api
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Dictionary<DateTime, GetCaseDataDto>, List<Case>>().ConvertUsing((src, dest) => {
                var result = new List<Case>();
                foreach(var kvp in src)
                {
                    result.Add(new Case
                    {
                        Total = kvp.Value.Total,
                        New = kvp.Value.New,
                        Date = kvp.Key.Date,
                    });
                }

                return result;
            });

            CreateMap<GetCountryDataDto, CountryData>();
            CreateMap<AddCountryDataDto, CountryData>();
            CreateMap<AddCaseDataDto, Case>();
            CreateMap<AddUserRoleDto, UserRole>();
            CreateMap<AddRolePermissionDto, RolePermission>();
            CreateMap<GetRolePermissionDto, RolePermission>();
            
            // Role Permission to GetRolePermissionDto
            CreateMap<RolePermission, GetRolePermissionDto>();

            CreateMap<UserRole,GetUserRoleDto>();
            CreateMap<User, GetUserDto>();

        }

    }
}

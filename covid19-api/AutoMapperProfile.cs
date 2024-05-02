﻿using AutoMapper;
using covid19_api.Dtos.CountryData;

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
                        Date = new DateTime()
                    });
                }

                return result;
            });

            CreateMap<GetCountryDataDto, CountryData>();
        }

    }
}
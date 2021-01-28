﻿using API.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductBrand ,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType ,o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl ,o=>o.MapFrom<ProductUrlResolver>());
        }
    }
}

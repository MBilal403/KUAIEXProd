using DataAccessLayer.Entities;
using KuaiexDashboard.DTO.Beneficiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Profile
{
    public class MapperProfile : AutoMapper.Profile
    {
        public static void Run()
        {

            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<MapperProfile>();
            });
        }
        public MapperProfile()
        {
            CreateMap<BeneficiaryDTO, Beneficiary>().ReverseMap();
        }
    }
}
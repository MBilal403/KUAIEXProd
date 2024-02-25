using DataAccessLayer.Entities;
using KuaiexDashboard.DTO.Beneficiary;
using KuaiexDashboard.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Profile
{
    public class MapperProfile : AutoMapper.Profile
    {
     
        public MapperProfile()
        {
            CreateMap<BeneficiaryDTO, Beneficiary>().ReverseMap();
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<EditCustomerDTO, Customer>().ReverseMap();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<MapperProfile>();
            });
        }
    }
}
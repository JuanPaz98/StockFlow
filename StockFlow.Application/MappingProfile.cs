using AutoMapper;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Features.Customer.Queries.GetAllCustomers;

namespace StockFlow.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, GetAllCustomersModel>().ReverseMap();
        }
    }
}

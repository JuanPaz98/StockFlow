using AutoMapper;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Features.Customer.Commands.CreateCustomer;
using StockFlow.Application.Features.Customer.Queries.GetAllCustomers;

namespace StockFlow.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, GetAllCustomersModel>().ReverseMap();
            CreateMap<CustomerEntity, CreateCustomerModel>().ReverseMap();

            CreateMap<CreateCustomerCommand, CustomerEntity>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.model.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.model.Email))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.model.Phone))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.model.Address));
        }
    }
}

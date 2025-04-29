using AutoMapper;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Features.Dtos.Categories;
using StockFlow.Application.Features.Dtos.Customers;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Features.Dtos.Payments;
using StockFlow.Application.Features.Dtos.Products;
using StockFlow.Application.Features.Dtos.Suppliers;
using StockFlow.Domain.Entities;

namespace StockFlow.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Customers
            CreateMap<CustomerEntity, CustomerRequestDto>().ReverseMap();
            CreateMap<CustomerEntity, CustomerRequestIdDto>().ReverseMap();
            CreateMap<CustomerEntity, CustomerResponseDto>().ReverseMap();
            #endregion

            #region Orders
            CreateMap<OrderEntity, OrderRequestDto>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailsDto>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailsIdDto>().ReverseMap();
            CreateMap<OrderEntity, OrderWithIdDto>()
                .ForMember(des => des.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails)).ReverseMap();
            #endregion

            #region Products
            CreateMap<ProductEntity, ProductRequestDto>().ReverseMap();
            CreateMap<ProductEntity, ProductRequestIdDto>().ReverseMap();
            CreateMap<ProductEntity, ProductResponseDto>().ReverseMap();
            #endregion

            #region Categories
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
            CreateMap<CategoryEntity, CategoryIdDto>().ReverseMap();
            #endregion

            #region Suppliers
            CreateMap<SupplierEntity, SupplierRequestDto>().ReverseMap();
            CreateMap<SupplierEntity, SupplierRequestIdDto>().ReverseMap();
            CreateMap<SupplierEntity, SupplierResponseDto>().ReverseMap();
            #endregion

            #region Payments
            CreateMap<PaymentEntity, PaymentRequestDto>().ReverseMap();
            CreateMap<PaymentEntity, PaymentResponseDto>().ReverseMap();
            #endregion
        }
    }
}

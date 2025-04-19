using AutoMapper;
using StockFlow.Api.Domain.Entities;
using StockFlow.Application.Features.Categories.Commands.CreateCategory;
using StockFlow.Application.Features.Customer.Commands.CreateCustomer;
using StockFlow.Application.Features.Customer.Commands.UpdateCustomer;
using StockFlow.Application.Features.Customer.Queries.GetAllCustomers;
using StockFlow.Application.Features.Customer.Queries.GetCustomerById;
using StockFlow.Application.Features.Dtos.Orders;
using StockFlow.Application.Features.Orders.Queries.GetOrderById;
using StockFlow.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using StockFlow.Application.Features.Payments.Commands.CreatePayment;
using StockFlow.Application.Features.Products.Commands.CreateProduct;
using StockFlow.Application.Features.Products.Commands.UpdateProduct;
using StockFlow.Application.Features.Products.Queries.GetAllProducts;
using StockFlow.Application.Features.Products.Queries.GetProductById;
using StockFlow.Application.Features.Suppliers.Commands.CreateSupplier;
using StockFlow.Application.Features.Suppliers.Commands.UpdateSupplier;
using StockFlow.Application.Features.Suppliers.Queries.GetAllSuppliers;
using StockFlow.Application.Features.Suppliers.Queries.GetSupplierById;
using StockFlow.Domain.Entities;

namespace StockFlow.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Customers
            CreateMap<CustomerEntity, GetAllCustomersModel>().ReverseMap();
            CreateMap<CustomerEntity, CreateCustomerModel>().ReverseMap();
            CreateMap<CreateCustomerCommand, CustomerEntity>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.model.Name))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.model.Email))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.model.Phone))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.model.Address)).ReverseMap();
            CreateMap<CustomerEntity, UpdateCustomerModel>().ReverseMap();
            CreateMap<CustomerEntity, GetCustomerByIdModel>().ReverseMap();
            #endregion

            #region Orders
            CreateMap<OrderEntity, OrderRequestDto>().ReverseMap();
            CreateMap<OrderEntity, OrderWithIdDto>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailsDto>().ReverseMap();
            CreateMap<OrderDetailEntity, OrderDetailsIdDto>().ReverseMap();
            CreateMap<OrderEntity, GetOrdersByCustomerIdModel>().ReverseMap();
            CreateMap<OrderEntity, GetOrderByIdModel>()
                .ForMember(des => des.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails)).ReverseMap();
            CreateMap<OrderEntity, OrderWithIdDto>()
                .ForMember(des => des.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails)).ReverseMap();
            #endregion

            #region Products
            CreateMap<ProductEntity, GetAllProductsModel>().ReverseMap();
            CreateMap<ProductEntity, CreateProductModel>().ReverseMap();
            CreateMap<ProductEntity, GetProductByIdModel>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductModel>().ReverseMap();
            CreateMap<ProductEntity, GetProductByCategoryModel>().ReverseMap();
            #endregion

            #region Categories
            CreateMap<CategoryEntity, GetProductByCategoryModel>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
            CreateMap<CategoryEntity, CreateCategoryModel>().ReverseMap();

            #endregion

            #region Suppliers
            CreateMap<SupplierEntity, GetSupplierByIdModel>().ReverseMap();
            CreateMap<SupplierEntity, GetAllSuppliersModel>().ReverseMap();
            CreateMap<SupplierEntity, CreateSupplierModel>().ReverseMap();
            CreateMap<SupplierEntity, UpdateSupplierModel>().ReverseMap();
            #endregion

            #region Payments
            CreateMap<PaymentEntity, CreatePaymentModel>().ReverseMap();
            #endregion
        }
    }
}

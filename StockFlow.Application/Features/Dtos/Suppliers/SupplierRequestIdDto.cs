namespace StockFlow.Application.Features.Dtos.Suppliers
{
    public record SupplierRequestIdDto(
       int Id,
       string Name,
       string Contact,
       string Phone
       );
}

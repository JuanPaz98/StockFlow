namespace StockFlow.Application.Features.Dtos.Suppliers
{
    public record SupplierResponseDto(
       int Id,
       string Name,
       string Contact,
       string Phone
       );
}

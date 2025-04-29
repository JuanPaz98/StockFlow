namespace StockFlow.Application.Features.Dtos.Suppliers
{
    public record SupplierRequestDto(
        string Name,
        string? Contact,
        string? Phone
        );
}

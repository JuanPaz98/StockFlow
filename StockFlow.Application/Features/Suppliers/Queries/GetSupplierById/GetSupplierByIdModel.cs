namespace StockFlow.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Contact { get; set; }
        public string? Phone { get; set; }
    }
}

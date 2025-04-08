namespace StockFlow.Application.Common.Constants
{
    public static class CacheKeys
    {
        // Customers
        public static string AllCustomers => "customers: all";
        public static string CustomerById(int id) => $"customers: {id}";

        // Products
        public static string AllProducts => "products: all"; 
        public static string ProductById(int id) => $"products: {id}"; 
        public static string ProductsByCategory(string category) => $"products: {category}"; 

        // Orders
        public static string OrdersByCustomerId(int id) => $"orders: customer{id}"; 
        public static string OrderById(int id) => $"orders: {id}"; 

        // Suppliers
        public static string AllSuppliers => "suppliers: all"; 
        public static string SupplierById(int id) => "suppliers: all"; 
    }
}

namespace StockFlow.Application.Common.Constants
{
    public static class CacheKeys
    {
        #region Customers
        public static string AllCustomers => "customers: all";
        public static string CustomerById(int id) => $"customers: {id}";
        #endregion

        #region Products
        public static string AllProducts => "products: all"; 
        public static string ProductById(int id) => $"products: {id}"; 
        public static string ProductsByCategory(string category) => $"products: {category}";
        #endregion

        #region Categories
        public static string AllCategories => "categories: all";
        public static string CategoryById(int id) => $"category: {id}";
        #endregion

        #region Customers
        public static string OrdersByCustomerId(int id) => $"orders: customer{id}"; 
        public static string OrderById(int id) => $"orders: {id}";
        #endregion

        #region Suppliers
        public static string AllSuppliers => "suppliers: all"; 
        public static string SupplierById(int id) => $"suppliers: {id}";
        #endregion

        #region Payments
        public static string AllPayments => "payments: all";
        public static string PaymentById(int id) => $"payment: {id}";
        public static string PaymentsByOrderId(int id) => $"payment: order-id-{id}";
        #endregion
    }
}

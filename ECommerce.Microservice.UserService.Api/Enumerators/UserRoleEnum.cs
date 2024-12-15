namespace ECommerce.Microservice.UserService.Api.Enumerators
{
    public enum UserRoleEnum : int
    {
        Admin = 1,       // Full access to the system, manage users, products, orders, etc.
        Customer = 2,    // Regular user who can browse and purchase products.
        Seller = 3,      // A vendor who can list and manage their own products.
        DeliveryAgent = 4, // Handles shipping and delivery of orders.
        Support = 5      // Customer support role to handle inquiries or complaints.
    }

    public static class UserRoleResources
    {
        public const string Admin = "Admin";               // Full access to the system
        public const string Customer = "Customer";         // Regular user
        public const string Seller = "Seller";             // Vendor who manages their own products
        public const string DeliveryAgent = "DeliveryAgent"; // Handles shipping and delivery
        public const string Support = "Support";           // Customer support
    }
}

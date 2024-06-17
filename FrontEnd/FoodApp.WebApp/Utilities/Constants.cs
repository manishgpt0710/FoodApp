namespace FoodApp.WebApp.Utilities
{
    public class Constants
    {
        public static string AuthAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum RoleType
        {
            ADMIN,
            CUSTOMER
        }
        public const string TokenCookie = "JWTToken";

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}

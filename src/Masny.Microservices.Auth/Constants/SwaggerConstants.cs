namespace Masny.Microservices.Auth.Constants
{
    /// <summary>
    /// Swagger constants.
    /// </summary>
    public class SwaggerConstants
    {
        /// <summary>
        /// Security.
        /// </summary>
        public static class Security
        {
            /// <summary>
            /// Description.
            /// </summary>
            public const string Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"";

            /// <summary>
            /// Name.
            /// </summary>
            public const string Name = "Authorization";

            /// <summary>
            /// Schema.
            /// </summary>
            public const string Schema = "Bearer";

            /// <summary>
            /// HTTP Authorization.
            /// </summary>
            public const string HttpAuth = "bearer";
        }
    }
}

using System;

namespace control_inventario_function.SoporteUtil
{
    public static class SettingEnvironment
    {
        private const string SqlConnectionStringKey = "SqlConnectionString";
        private const string SqlJwtSecretKey = "JWT_SECRET_KEY";
        private const string SqlJwtAudienceKey = "JWT_AUDIENCE_TOKEN";
        private const string SqlJwtIssuerKey = "JWT_ISSUER_TOKEN";
        private const string SqlJwtExpireKey = "JWT_EXPIRE_MINUTES";

        private const string BlobStorageConnectionString = "BlobStorageConnectionString";
        private const string BlobStorageContainerName = "BlobStorageContainerName";
        public static string GetSqlConnectionString()
        {
            return GetEnvironmentVariable(SqlConnectionStringKey);
        }
        public static string GetJWTSecretKey()
        {
            return GetEnvironmentVariable(SqlJwtSecretKey);
        }
        public static string GetJWTAudienceKey()
        {
            return GetEnvironmentVariable(SqlJwtAudienceKey);
        }
        public static string GetJWTIssuerKey()
        {
            return GetEnvironmentVariable(SqlJwtIssuerKey);
        }
        public static string GetJWTExpireKey()
        {
            return GetEnvironmentVariable(SqlJwtExpireKey);
        }
        public static string GetBlobStorageConnectionString()
        {
            return GetEnvironmentVariable(BlobStorageConnectionString);
        }
        public static string GetBlobStorageContainerName()
        {
            return GetEnvironmentVariable(BlobStorageContainerName);
        }

        private static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);

        }

    }
}

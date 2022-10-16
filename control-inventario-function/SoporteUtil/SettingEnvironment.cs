using System;

namespace control_inventario_function.SoporteUtil
{
    public static class SettingEnvironment
    {
        private const string SqlConnectionStringKey = "SqlConnectionString";
        public static string GetSqlConnectionString()
        {
            return GetEnvironmentVariable(SqlConnectionStringKey);
        }

        private static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);

        }

    }
}

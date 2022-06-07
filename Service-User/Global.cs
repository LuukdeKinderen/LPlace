namespace Service_User
{
    public static class Global
    { 
        public static readonly string RabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
        public static readonly string RabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");

        private static readonly string DatabaseServer = Environment.GetEnvironmentVariable("DATABASE_SERVER");
        private static readonly string DatabasePassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        private static readonly string DatabasePort = Environment.GetEnvironmentVariable("DATABASE_PORT");

        public static readonly string DatabaseConnectionString = $"Server={DatabaseServer},{DatabasePort};Database=advertisement;User Id=sa;Password={DatabasePassword};";

    }
}

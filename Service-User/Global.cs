namespace Service_User
{
    public static class Global
    { 
        public static readonly string RabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
        public static readonly string RabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
    }
}

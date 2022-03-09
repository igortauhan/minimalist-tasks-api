namespace MinimalistTasks.Services;

public static class JwtKeyService
{
    public static string GetKey()
    {
        IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        return configurationRoot.GetValue<string>("Jwt:Key");
    }
}
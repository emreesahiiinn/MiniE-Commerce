using Microsoft.Extensions.Configuration;

namespace Core.Configuration;

public static class Configuration
{
    private static readonly string ShortPath = "../../Presentation/BigiBogy.WebApi";
    private static readonly string JsonFileName = "appsettings.json";

    public static string PostgreSQLConnectionString
    {
        get
        {
            var configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), ShortPath));
            configurationManager.AddJsonFile(JsonFileName);
            return configurationManager.GetConnectionString("PostgresSQL");
        }
    }
}
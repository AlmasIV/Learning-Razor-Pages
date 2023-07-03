using Microsoft.Data.SqlClient;

namespace Learning_Razor_Pages.Model.DefaultConnectionString;

public class DefaultConnectionString {
    public SqlConnectionStringBuilder Builder;
    public DefaultConnectionString(){
        IConfigurationBuilder configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot config = configBuilder.Build();
        Builder = new SqlConnectionStringBuilder();
        Builder.ConnectionString = config.GetConnectionString("DefaultConnectionString");
    }
}
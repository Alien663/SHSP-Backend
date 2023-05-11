using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Tokenization
{
    public class AppDB : IDisposable
    {
        public SqlConnection Connection;
        public AppDB(string ? ConnectionName = null)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot root = builder.Build();
            ConnectionName ??= root["ConnectionStrings:Default"];
            Connection = new SqlConnection(ConnectionName);
        }
        public void Dispose()
        { 
            Connection.Close();
            Connection.Dispose();
        }
    }
}

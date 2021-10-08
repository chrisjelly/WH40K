using Autofac;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace JellyDev.WH40K.Api
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {            
            // Register database connection for Dapper queries
            builder.Register<DbConnection>(x => new SqlConnection(_connectionString));
        }
    }
}

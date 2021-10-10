using Autofac;
using Microsoft.Data.SqlClient;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data.Common;

namespace JellyDev.WH40K.Api
{
    public class AutofacModule : Module
    {
        /// <summary>
        /// Read-only connection string for Dapper queries
        /// </summary>
        private readonly string _connectionStringRead;

        /// <summary>
        /// Create the Autofac API module
        /// </summary>
        /// <param name="connectionStringRead">Read-only connection string for Dapper queries</param>
        public AutofacModule(string connectionStringRead)
        {
            _connectionStringRead = connectionStringRead;
        }

        protected override void Load(ContainerBuilder builder)
        {            
            // Register database connection for Dapper queries
            builder.Register<DbConnection>(x => new SqlConnection(_connectionStringRead))
                .InstancePerLifetimeScope();
        }
    }
}

using eValuate.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evaluationapi.Dependency
{
    public static  class ServiceConfigurationExtension
    {
        public static void AddApplicationSpecificServices(IServiceCollection services, IConfiguration configuration)
        {
            AddSingletonServices(services);
            AddTransiantServices(services);
        }
        private static void AddSingletonServices(IServiceCollection services)
        {
            services.AddLogging(logging => logging.AddConsole());
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
        }

        private static void AddTransiantServices(IServiceCollection services)
        {
            services.AddTransient<IDbConnectionProvider, DbConnectionProvider>();
            services.AddTransient<IDapperSqlProvider, DapperSqlProvider>();
            services.AddTransient<IQuestionTypeRepository, QuestionTypeRepository>();
        }

    }
}

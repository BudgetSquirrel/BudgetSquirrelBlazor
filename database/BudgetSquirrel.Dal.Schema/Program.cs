using System;
using System.Collections.Generic;
using System.Linq;
using BudgetSquirrel.Dal.Schema.Auth;
using BudgetSquirrel.Dal.Schema.StoredProcedures;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetSquirrel.Dal.Schema
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = GetConfigurations(args);
            var serviceProvider = CreateServices(config);

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        
        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(IConfiguration config)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(config.GetConnectionString("DefaultConnection"))
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Initial).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddSingleton(config)
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();

            // Execute the migrations
            runner.MigrateUp();

            CreateStoredProcedures.CreateRegisteredProcedures(config.GetConnectionString("DefaultConnection"));
        }

        private static IConfiguration GetConfigurations(string[] args)
        {
            string envName = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            return config;
        }
    }
}

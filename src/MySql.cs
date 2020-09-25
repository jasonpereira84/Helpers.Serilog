using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Serilog
    {
        namespace MySql
        {
            using global::MySql.Data.MySqlClient;

            public sealed class Context : _Context<AppSettings.Database.MySql, MySqlConnection, MySqlCommand>
            {
                public static Generator.ConnectionString<AppSettings.Database.MySql> DefaultConnectionStringGenerator
                    => new Generator.ConnectionString<AppSettings.Database.MySql>((mySql)
                            => new StringBuilder($"Database={mySql.Name};")
                                .Append($"Server={mySql.Server.Endpoint.Host};Port={mySql.Server.Endpoint.Port};")
                                .Append($"User Id={mySql.Server.Credentials.Id};Password={mySql.Server.Credentials.Secret};")
                                .Append($"PersistSecurityInfo={mySql.PersistSecurityInfo};")
                                .Append($"AllowUserVariables={mySql.AllowUserVariables};")
                                .ToString());

                public static Generator.CommandText DefaultCommandTextGenerator(String format, String table)
                    => new Generator.CommandText((timestamp, level, messageTemplate, exception, parameters)
                        => format.Interpolate(table, timestamp, level, messageTemplate, exception, parameters));
                public static Generator.CommandText DefaultCommandTextGenerator(String table)
                    => DefaultCommandTextGenerator("INSERT INTO {Table} (Timestamp, Level, Message, Exception, Properties) VALUES ({Timestamp}, {Level}, {MessageTemplate}, {Exception}, {Properties})", table);

                public Context(String connectionString, Generator.CommandText commandTextGenerator)
                    : base(connectionString, commandTextGenerator)
                { }

                public Context(AppSettings.Database.MySql mySql, Generator.ConnectionString<AppSettings.Database.MySql> connectionStringGenerator, Generator.CommandText commandTextGenerator)
                    : base(mySql, connectionStringGenerator, commandTextGenerator)
                { }

                public static Context CommonDatabase(AppSettings.Database.MySql mySql, String table)
                    => new Context(mySql, DefaultConnectionStringGenerator, DefaultCommandTextGenerator(table));

                public static Context CommonDatabase(String host, String secret, String table)
                    => CommonDatabase(
                        new AppSettings.Database.MySql
                        {
                            Name = "common",
                            Provider = "MySql.Data.MySqlClient",
                            Server = new AppSettings.Server
                            {
                                Endpoint = new AppSettings.Endpoint
                                {
                                    Host = host,
                                    Port = 3306
                                },
                                Credentials = new AppSettings.Credentials
                                {
                                    Id = "app.common",
                                    Secret = secret
                                }
                            }
                        }, table);
            }
        }
    }
}
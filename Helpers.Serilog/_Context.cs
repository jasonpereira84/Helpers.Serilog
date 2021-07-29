using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;

    namespace Serilog
    {
        public abstract class _Context<TConnection, TCommand>
            where TConnection : IDbConnection, new()
            where TCommand : IDbCommand, new()
        {
            protected _Context(String connectionString, Generator.CommandText commandTextGenerator)
            {
                ConnectionString = connectionString.SanitizeTo(null) ?? throw new ArgumentNullException(nameof(connectionString));
                CommandTextGenerator = commandTextGenerator ?? throw new ArgumentNullException(nameof(commandTextGenerator));
                DefaultParameters = new Dictionary<String, String>();
            }

            protected String ConnectionString { get; private set; }

            protected Generator.CommandText CommandTextGenerator { get; private set; }

            protected Dictionary<String, String> DefaultParameters { get; private set; }

            public _Context<TConnection, TCommand> AddDefaultParameters(params (String Name, String Value)[] parameters)
            {
                foreach (var pair in (parameters ?? new (String Name, String Value)[0]))
                    if (!String.IsNullOrWhiteSpace(pair.Name))
                    {
                        var key = pair.Name.Trim();
                        if (!DefaultParameters.ContainsKey(key))
                            DefaultParameters.Add(key, pair.Value.Sanitize().EscapeSingleQuotes());
                    }
                return this;
            }

            public TConnection CreateConnection()
            {
                var connection = new TConnection();
                connection.ConnectionString = ConnectionString;
                return connection;
            }

            public (String Name, String Value)[] WriteEvent(TConnection connection, DateTimeOffset timestamp, LogEventLevel level, Exception exception, String messageTemplate, params (String Name, String Value)[] parameters)
            {
                (String Name, String Value)[] _getAllParameters()
                {
                    var @params = new Dictionary<String, String>(DefaultParameters);
                    foreach (var pair in (parameters ?? new (String Name, String Value)[0]))
                        if (!String.IsNullOrWhiteSpace(pair.Name))
                        {
                            var key = pair.Name.Trim();
                            var value = pair.Value.Sanitize().EscapeSingleQuotes();
                            if (!@params.ContainsKey(key))
                                @params.Add(key, value);
                            else
                                @params[key] = value;
                        }
                    return @params.Select(pair => (Name: pair.Key, Value: pair.Value)).ToArray();
                }

                var allParameters = _getAllParameters();
                using (var command = new TCommand())
                {
                    command.Connection = connection;
                    command.CommandText = CommandTextGenerator.Invoke(timestamp, level, messageTemplate, exception, allParameters);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return allParameters;
            }

            public (String Name, String Value)[] WriteEvent(TConnection connection, DateTimeOffset timestamp, LogEventLevel level, String messageTemplate, params (String Name, String Value)[] parameters)
                => WriteEvent(connection, timestamp, level, null, messageTemplate, parameters);

            public (String Name, String Value)[] WriteEvent(DateTimeOffset timestamp, LogEventLevel level, String messageTemplate, params (String Name, String Value)[] parameters)
            {
                var allParameters = new (String Name, String Value)[0];
                using (var connection = CreateConnection())
                    allParameters = WriteEvent(connection, timestamp, level, messageTemplate, parameters);
                return allParameters;
            }

            public (String Name, String Value)[] WriteEvent(DateTimeOffset timestamp, LogEventLevel level, Exception exception, String messageTemplate, params (String Name, String Value)[] parameters)
            {
                var allParameters = new (String Name, String Value)[0];
                using (var connection = CreateConnection())
                    WriteEvent(connection, timestamp, level, exception, messageTemplate, parameters);
                return allParameters;
            }

        }

        public abstract class _Context<TDatabase, TConnection, TCommand> : _Context<TConnection, TCommand>
            where TDatabase : AppSettings.Database._Base
            where TConnection : IDbConnection, new()
            where TCommand : IDbCommand, new()
        {
            protected _Context(String connectionString, Generator.CommandText commandTextGenerator)
                : base(connectionString, commandTextGenerator)
            { }

            //Order flipped for ctor overloading
            private _Context(Generator.CommandText commandTextGenerator, TDatabase database, Generator.ConnectionString<TDatabase> connectionStringGenerator)
                : this(connectionStringGenerator.Invoke(database), commandTextGenerator)
            { }
            protected _Context(TDatabase database, Generator.ConnectionString<TDatabase> connectionStringGenerator, Generator.CommandText commandTextGenerator)
                : this(commandTextGenerator,
                      database ?? throw new ArgumentNullException(nameof(database)),
                      connectionStringGenerator ?? throw new ArgumentNullException(nameof(connectionStringGenerator)))
            { }
        }
    }
}
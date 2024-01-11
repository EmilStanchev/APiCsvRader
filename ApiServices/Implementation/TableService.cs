using ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Implementation
{
    public class TableService : ITableService
    {
        public T SelectByID<T>(string id,string table,string columnName, string connectionString)
        {
            string query = $"SELECT * FROM {table} WHERE {columnName} = @Id";
            T item = default(T);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = CreateInstance<T>(reader);

                            foreach (var property in typeof(T).GetProperties())
                            {
                                SetValueIfNotNull(item, property, reader[property.Name]);
                            }
                        }
                    }
                }
            }

            return item;
        }

        private T CreateInstance<T>(IDataReader reader)
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                throw new InvalidOperationException($"No parameterless constructor defined for type {typeof(T)}.");
            }

            var parameters = GetParametersFromReader<T>(reader, constructor.GetParameters());
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }

        private object[] GetParametersFromReader<T>(IDataReader reader, ParameterInfo[] parameters)
        {
            return parameters.Select(parameter =>
            {
                var columnName = parameter.Name;
                var value = reader[columnName];
                return Convert.ChangeType(value, parameter.ParameterType);
            }).ToArray();
        }

        private void SetValueIfNotNull<T>(T item, PropertyInfo property, object value)
        {
            if (property.GetSetMethod() != null && value != DBNull.Value)
            {
                property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
            }
        }
    }
}

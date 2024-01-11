using ApiServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices.Implementation
{
    public class TableService : ITableService
    {
        public T GetEntityById<T>(int entityId, string tableName, string columnName, string connectionString)
            {
            return GetEntityById<T>(entityId.ToString(), tableName, columnName, connectionString);
        }

        public T GetEntityById<T>(string entityId, string tableName, string columnName, string connectionString)
        {
            T entity = default;

            string sql = $"SELECT * FROM {tableName} WHERE  {columnName}  = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", entityId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entity = MapDataReaderToEntity<T>(reader);
                        }
                    }
                }
            }

            return entity;
        }

        private T MapDataReaderToEntity<T>(SQLiteDataReader reader)
        {
            T entity = Activator.CreateInstance<T>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetName(i);
                object value = reader.GetValue(i);

                PropertyInfo property = typeof(T).GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    object convertedValue = Convert.ChangeType(value, targetType);
                    property.SetValue(entity, convertedValue);
                }
            }

            return entity;
        }
    }
}

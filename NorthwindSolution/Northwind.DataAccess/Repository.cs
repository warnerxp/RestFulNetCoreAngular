using Dapper.Contrib.Extensions;
using Northwind.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //protected string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=True";
        protected string _connectionString = "Server=tcp:warnerxp-azure2.database.windows.net,1433;Initial Catalog = Northwind; Persist Security Info=False;User ID = warnerxp; Password=W2008extreme4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";
        
        public Repository(string connectionString)
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"{type.Name}"; };
            _connectionString = "Server=tcp:warnerxp-azure2.database.windows.net,1433;Initial Catalog = Northwind; Persist Security Info=False;User ID = warnerxp; Password=W2008extreme4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";
        }
        public bool Delete(T entity)
        {
            using ( var connection = new SqlConnection(_connectionString))
            {
                return connection.Delete(entity);
            }
        }

        public T GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Get<T>(id);
            }
        }

        public IEnumerable<T> GetList()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<T>();
            }
        }

        public int Insert(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return (int) connection.Insert(entity);
            }
        }

        public bool Update(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Update(entity);
            }
        }
    }
}

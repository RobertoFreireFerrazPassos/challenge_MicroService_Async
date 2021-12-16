using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using ApiUser.Domain.Entities;

namespace ApiUser.DataAccess.Repositories.Base
{
	public class Repository<T> where T : Entity
	{
        private readonly IConfiguration configuration;
		public Repository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private SqlConnection OpenDbConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("mssqlserverConnection"));
        }

		public async Task<IEnumerable<T>> ExecuteAsync(string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return await sqlConnection.QueryAsync<T>(query);
				}
				catch (Exception exception)
				{
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task AddAsync(T entity, string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					await sqlConnection.ExecuteAsync(query, entity);
				}
				catch (Exception exception)
				{
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> UpdateAsync(T entity, string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return await sqlConnection.ExecuteAsync(query, entity);
				}
				catch (Exception exception)
				{
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> UpdateAsync(string query, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return await sqlConnection.ExecuteAsync(query, AddParameters(parameter));
				}
				catch (Exception exception)
				{
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task DeleteAsync(string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					await sqlConnection.ExecuteAsync(query);
				}
				catch (Exception exception)
				{
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		private IDictionary<string, object> AddParameters(params (string, object)[] parameter)
		{
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (var item in parameter)
			{
				dictionary.Add(item.Item1, item.Item2);
			}

			return dictionary;
		}
    }
}
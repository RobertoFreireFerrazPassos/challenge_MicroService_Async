using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPortalPersistence.DataAccess.Repositories.Base
{
    public class todelete<T> : IRepository<T> where T : Entity
	{
		private readonly IConfiguration _configuration;
		private readonly IApplicationLogger _applicationLogger;

		public todelete(
			IConfiguration configuration,
			IApplicationLogger applicationLogger)
		{
			_configuration = configuration ??
				throw new ArgumentNullException(nameof(configuration));
			_applicationLogger = applicationLogger ??
				throw new ArgumentNullException(nameof(applicationLogger));
		}

		public NpgsqlConnection OpenDbConnection()
		{
			return new NpgsqlConnection(_configuration[Constants.ConnectionString]);
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
					_applicationLogger.LogError(exception, exception.Message);
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
					_applicationLogger.LogError(exception, exception.Message);
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
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task DeleteAsync(Guid id, string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					await sqlConnection.ExecuteAsync(query, new { Id = id });
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
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
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> ExecuteAsync(string query, object parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return await sqlConnection.ExecuteAsync(query, parameter);
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> ExecuteAsync(string query, params (string, object)[] parameter)
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
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> ExecuteAsync(string query, CommandType commandType, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return await sqlConnection.ExecuteAsync(query, AddParameters(parameter), commandType: commandType);
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> ExecuteProcWithSqlParameterAsync(string query, string parameterName, string parameterTypeName, DataTable dataTable)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					var parameter = new SqlParameter(parameterName, SqlDbType.Structured)
					{
						TypeName = parameterTypeName,
						Value = dataTable
					};

					var sqlCommand = sqlConnection.CreateCommand();
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandText = query;
					sqlCommand.Parameters.Add(parameter);

					return sqlCommand.ExecuteNonQuery();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task DeleteAsync(string query, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					await sqlConnection.ExecuteAsync(query, AddParameters(parameter));
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<T> FindByIdAsync(Guid id, string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return (await sqlConnection.QueryAsync<T>(
						query, new { Id = id })).FirstOrDefault();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<Z> FindByIdAsync<Z>(Guid id, string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return (await sqlConnection.QueryAsync<Z>(
						query, new { Id = id })).FirstOrDefault();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}
		public async Task<IEnumerable<dynamic>> FindAllAsyncDynamic(string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return (await sqlConnection.QueryAsync(
						query)).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<T>> FindAllAsync(string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return (await sqlConnection.QueryAsync<T>(
						query)).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<T>> FindAllAsync(string query, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();
					return (await sqlConnection.QueryAsync<T>(query, AddParameters(parameter))).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<T>> FindAllAsync(string query, CommandType commandType, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();
					return (await sqlConnection.QueryAsync<T>(query, AddParameters(parameter), commandType: commandType)).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<Z>> FindAllAsync<Z>(string query, CommandType commandType, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();
					return (await sqlConnection.QueryAsync<Z>(query, AddParameters(parameter), commandType: commandType)).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
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

		public async Task<int> CountAsync(string query, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();
					var result = await sqlConnection.ExecuteScalarAsync<int>(query, AddParameters(parameter));
					return result;
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<int> CountAsync(string query, CommandType commandType, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();
					var result = await sqlConnection.ExecuteScalarAsync<int>(query, AddParameters(parameter), commandType: commandType);
					return result;
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<Z> ExecuteScalarAsync<Z>(string query, params (string, object)[] parameter)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();
					var result = await sqlConnection.ExecuteScalarAsync<Z>(query, AddParameters(parameter));
					return result;
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<Tuple<T, T2, T3, T4, T5, T6>>> QueryJoinedAsync<T2, T3, T4, T5, T6>(string sql, string splitOn = "Id", object param = null)
		where T2 : Entity
		where T3 : Entity
		where T4 : Entity
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();

					var result = await sqlConnection.QueryAsync<T, T2, T3, T4, T5, T6, Tuple<T, T2, T3, T4, T5, T6>>(sql, (t, t2, t3, t4, t5, t6) => Tuple.Create(t, t2, t3, t4, t5, t6), param, splitOn: splitOn);

					return result.ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<Tuple<T, T2>>> QueryJoinedAsync<T2>(string sql, string splitOn = "Id", object param = null)
		where T2 : Entity
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					sqlConnection.Open();

					var result = await sqlConnection.QueryAsync<T, T2, Tuple<T, T2>>(sql, (t, t2) => Tuple.Create(t, t2), param, splitOn: splitOn);

					return result.ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public async Task<IEnumerable<Z>> QueryAllAsync<Z>(string query)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					await sqlConnection.OpenAsync();
					return (await sqlConnection.QueryAsync<Z>(
						query)).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		public List<SplitListItem<T>> Split<T>(List<T> sourceList, int pageSize)
		{
			List<SplitListItem<T>> resultList = new List<SplitListItem<T>>();

			if (sourceList != null && sourceList.Any())
			{
				int pages = (sourceList.Count / pageSize) + 1;
				int mod = sourceList.Count % pageSize;
				int firstRecord = 0;

				List<T> childList = new List<T>();
				for (int i = 1; i <= pages; i++)
				{
					childList = new List<T>(sourceList.GetRange(firstRecord, i == pages ? mod : pageSize));

					resultList.Add(new SplitListItem<T>() { SplitList = childList });

					firstRecord += pageSize;
				}
			}

			return resultList;
		}

		public async Task<IEnumerable<T>> FindAllPaginatedAsync(string query, int pageIndex, int pageSize,
			params (string, object)[] parameters)
		{
			using (var sqlConnection = OpenDbConnection())
			{
				try
				{
					var paginatedQuery = $@"{query} OFFSET @offset ROWS
                                            FETCH NEXT @pageSize ROWS ONLY;";
					await sqlConnection.OpenAsync();
					var paramsDict = AddParameters(parameters);
					paramsDict.Add("offset", (pageIndex) * pageSize);
					paramsDict.Add("pageSize", pageSize);
					return (await sqlConnection.QueryAsync<T>(
						paginatedQuery, paramsDict)).ToList();
				}
				catch (Exception exception)
				{
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		protected SqlMapper.ICustomQueryParameter CreateTableTypeParameterWithSingleColumn<Z>(IEnumerable<Z> list, string tableName, string columnName)
		{
			Type type = typeof(Z);

			if (!type.IsPrimitive && type != typeof(string) && type != typeof(Guid))
			{
				throw new ArgumentException("List type must be a primitive type");
			}

			var dataTable = new DataTable(tableName);
			dataTable.Columns.Add(columnName, type);

			if (list != null)
			{
				foreach (var value in list)
				{
					dataTable.Rows.Add(value);
				}
			}

			return CreateTableValuedParameter(dataTable, tableName);
		}

		protected SqlMapper.ICustomQueryParameter CreateTableValuedParameter(DataTable dataTable, string tableName)
		{
			return dataTable.AsTableValuedParameter(tableName);
		}

		public async Task<int> UpdateRangeAsync<Z>(IEnumerable<Z> entity, string query)
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
					_applicationLogger.LogError(exception, exception.Message);
					throw exception;
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}
	}
}
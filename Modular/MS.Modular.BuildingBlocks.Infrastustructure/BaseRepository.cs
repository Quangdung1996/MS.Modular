using Dapper;
using MS.Modular.BuildingBlocks.Domain;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Modular.BuildingBlocks.Infrastustructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly string _connectionString;

        public readonly AsyncRetryPolicy _retryPolicy;
        private readonly int[] _sqlExceptions = new[] { 53, -2 };
        private const int RetryCount = 3;
        private const int WaitBetweenRetriesInSeconds = 15;
        public BaseRepository(string connectionString)
        {
            _retryPolicy = Policy.Handle<SqlException>(exception => _sqlExceptions.Contains(exception.Number))
                                 .WaitAndRetryAsync(retryCount: RetryCount,
                                                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(WaitBetweenRetriesInSeconds));

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async virtual Task<int> ExecuteAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.ExecuteAsync(query, parameters, commandType: commandType);
                    }
                });
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
                throw;
            }
            catch (Exception)
            {
                throw;
                // Handle generic ones here.
            }
        }

        public async virtual Task<ReturnResponse<T>> InsertAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            var returnResponse = new ReturnResponse<T>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        returnResponse.Data = await conn.QuerySingleAsync(query, parameters, commandType: commandType);
                        returnResponse.Succeeded = true;
                        return returnResponse;
                    }
                });
            }
            catch (Exception ex)
            {
                returnResponse.Succeeded = false;
                returnResponse.Error = ex.Message.ToString();
                return returnResponse;
            }
        }

        public async virtual Task<IEnumerable<T>> QueryAsync(string query)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.QueryAsync<T>(query, default);
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<IEnumerable<T>> QueryAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        return await conn.QueryAsync<T>(query, parameters, commandType: commandType);
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async virtual Task<ReturnResponse<T>> QueryFirstOrDefaultAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            var returnResponse = new ReturnResponse<T>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        returnResponse.Data = await conn.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);
                        returnResponse.Succeeded = true;
                        return returnResponse;
                    }
                });
            }
            catch (Exception ex)
            {
                returnResponse.Succeeded = false;
                returnResponse.Error = ex.Message.ToString();
                return returnResponse;
            }
        }

        public async virtual Task<ReturnResponse<bool>> UpdateAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            var returnResponse = new ReturnResponse<bool>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    using (var conn = CreateConnection())
                    {
                        returnResponse.Data = (await conn.ExecuteAsync(query, parameters, commandType: commandType)) > 0;
                        returnResponse.Succeeded = true;
                        return returnResponse;
                    }
                });
            }
            catch (Exception ex)
            {
                returnResponse.Succeeded = false;
                returnResponse.Error = ex.Message.ToString();
                return returnResponse;
            }
        }
    }
}
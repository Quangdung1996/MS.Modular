using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MS.Modular.BuildingBlocks.Domain
{
    public interface IBaseRepository<T>
    {
        Task<ReturnResponse<T>> InsertAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<ReturnResponse<bool>> UpdateAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> QueryAsync(string query);

        Task<IEnumerable<T>> QueryAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<int> ExecuteAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        Task<ReturnResponse<T>> QueryFirstOrDefaultAsync(string query, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
    }
}
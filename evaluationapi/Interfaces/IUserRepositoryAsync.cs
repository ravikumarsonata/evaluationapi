using Dapper;
using eValuate.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.WebApi.Interfaces
{
    public interface IUserRepositoryAsync
    {
        Task<IEnumerable<User>> GetAllUsers();

        ValueTask<User> GetUserById(int id);

        T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        
    }
}

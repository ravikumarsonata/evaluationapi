using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eValuate.WebApi.Interfaces;
using eValuate.WebApi.Models;
using eValuate.WebApi.Services.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace eValuate.WebApi.Repositories
{
    public class UserRepositoryAsync: GenericRepositoryAsync, IUserRepositoryAsync
    {
        private readonly ICommandText _commandText;

        private readonly IConfiguration _configuration;

        public UserRepositoryAsync(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
            _configuration = configuration;

        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {

            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<User>(_commandText.GetUsers);
                return query;
            });

        }

        public async ValueTask<User> GetUserById(int Id)
        {
            return await WithConnection(async c => {
                var p = new DynamicParameters();
                p.Add("Id", Id, DbType.Int64);
                var user = await c.QueryAsync<User>(
                    sql: "sp_User_GetById",
                    param: p,
                    commandType: CommandType.StoredProcedure);
                return user.FirstOrDefault();

            });
        }


        public List<T> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return db.Query<T>(query, sp_params, commandType: commandType).ToList();
        }
        public T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<T>(query, sp_params, commandType: commandType, transaction: transaction);
                    result = sp_params.Get<T>("retVal"); //get output parameter value
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            };
            return result;
        }

    }
}

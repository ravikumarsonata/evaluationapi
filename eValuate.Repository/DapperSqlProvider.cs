using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace eValuate.Repository
{
    public interface IDapperSqlProvider
    {
        Task<List<T>> DbQuery<T>(string procedurename, DynamicParameters dynamicParameters);
    }
    public class DapperSqlProvider : IDapperSqlProvider
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        public DapperSqlProvider(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<List<T>> DbQuery<T>(string procedurename, DynamicParameters dynamicParameters)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionProvider.ConnectionString))
            {
                return (await connection.QueryAsync<T>(procedurename, dynamicParameters, null, null, CommandType.StoredProcedure)).AsList();
            }
        }

    }
}

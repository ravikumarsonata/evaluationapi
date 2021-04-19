using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;

namespace eValuate.Repository
{
    public class QuestionnaireRepository : GenericRepositoryAsync, IQuestionnaire
    {
        private readonly ICommandText _commandText;

        private readonly IConfiguration _configuration;

        public QuestionnaireRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
            _configuration = configuration;

        }
        public T createQuestionnaire<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    result = dbConnection.Query<T>(query, sp_params, commandType: commandType, transaction: transaction).FirstOrDefault();
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
        public List<T> getQnrList<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return db.Query<T>(query, sp_params, commandType: commandType).ToList();
        }
        public async Task createFFQTemplate(int qnrRef, int copyFrom, string qnrRefString, string copyFromString)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(_configuration.GetConnectionString("WorkmateConnection")))
            {
                await dbConnection.ExecuteAsync(_commandText.AddFFQTemplate,
                    new { QnrRef = qnrRef, CopyFrom = copyFrom, QnrRefStr = qnrRefString, CopyFromStr = copyFromString });
            };
        }

    }
}

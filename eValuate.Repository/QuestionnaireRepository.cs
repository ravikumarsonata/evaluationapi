using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;
using eValuate.Domain;

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
        public async Task questionnaireInsert(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    await dbConnection.QueryAsync(query, sp_params, commandType: commandType, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            };
        }
        public List<QuestionnaireHeader> getQnrList<QuestionnaireHeader>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return db.Query<QuestionnaireHeader>(query, sp_params, commandType: commandType).ToList();
        }
        public List<Rpt_Questionnaire> getQtnList<Rpt_Questionnaire>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return db.Query<Rpt_Questionnaire>(query, sp_params, commandType: commandType).ToList();
        }
        public async Task createFFQTemplate(int qnrRef, int copyFrom, string qnrRefString, string copyFromString)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(_configuration.GetConnectionString("WorkmateConnection")))
            {
                await dbConnection.ExecuteAsync(_commandText.AddFFQTemplate,
                    new { QnrRef = qnrRef, CopyFrom = copyFrom, QnrRefStr = qnrRefString, CopyFromStr = copyFromString });
            };
        }
        public async Task createMOTLayout(int qnrRef, int copyFrom, string qnrRefString, string copyFromString)
        {
            using (MySqlConnection dbConnection = new MySqlConnection(_configuration.GetConnectionString("WorkmateConnection")))
            {
                await dbConnection.ExecuteAsync(_commandText.AddMOTLayout,
                    new { QnrRef = qnrRef, CopyFrom = copyFrom, QnrRefStr = qnrRefString, CopyFromStr = copyFromString });
            };
        }
        public int createNewTotalScoreMotText(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            int result;
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("i18nConnection")))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<int>(query, sp_params, commandType: commandType, transaction: transaction);
                    result = sp_params.Get<int>("textId"); //get output parameter value
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
        public List<MLToolLock_ChangeInfo> getMLToolLockChangeInfo<MLToolLock_ChangeInfo>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return db.Query<MLToolLock_ChangeInfo>(query, sp_params, commandType: commandType).ToList();
        }
    }
}

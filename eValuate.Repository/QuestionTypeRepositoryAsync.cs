using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Repository
{
    public class QuestionTypeRepositoryAsync : GenericRepositoryAsync, IQuestionTypeRepositoryAsync 
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public QuestionTypeRepositoryAsync(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<QuestionType> GetAll<QuestionType>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<QuestionType>(query, sp_params, commandType: commandType).ToList();
        }

        public QuestionType execute_sp<QuestionType>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            QuestionType result;
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<QuestionType>(query, sp_params, commandType: commandType, transaction: transaction);
                    result = sp_params.Get<QuestionType>("retVal");
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

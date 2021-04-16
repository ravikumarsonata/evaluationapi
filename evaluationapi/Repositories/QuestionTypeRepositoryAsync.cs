using Dapper;
using eValuate.WebApi.Repositories;
using evaluationapi.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace evaluationapi.Repositories
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

        public List<T> GetAll<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<T>(query, sp_params, commandType: commandType).ToList();
        }

        public T execute_sp<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<T>(query, sp_params, commandType: commandType, transaction: transaction);
                    result = sp_params.Get<T>("retVal");
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

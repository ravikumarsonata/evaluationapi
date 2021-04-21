using Dapper;
using eValuate.Domain;
using eValuate.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Repository
{
    public class QuestionTypeRepository : IQuestionTypeRepository 
    {
        private readonly IDapperSqlProvider _dapperSqlProvider;

        public QuestionTypeRepository(IDapperSqlProvider dapperSqlProvider, IConfiguration configuration)
        {
            _dapperSqlProvider = dapperSqlProvider;
        }

        /// <summary>
        /// Get all datta without parameter
        /// </summary>
        /// <returns></returns>
        public async Task<List<QuestionType>> GetAll()
        {
            DynamicParameters queryParameters = new DynamicParameters();
            return await _dapperSqlProvider.DbQuery<QuestionType>(StoredProcedureConstants.GetQuestionType, queryParameters);
        }

    }
}

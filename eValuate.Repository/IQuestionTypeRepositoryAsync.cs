using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Repository
{
    public interface IQuestionTypeRepositoryAsync
    {
        List<QuestionType> GetAll<QuestionType>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);

        QuestionType execute_sp<QuestionType>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);

    }
}

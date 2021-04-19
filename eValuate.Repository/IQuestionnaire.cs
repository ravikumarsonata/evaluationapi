using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Repository
{
    public interface IQuestionnaire
    {
        T createQuestionnaire<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<T> getQnrList<T>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        Task createFFQTemplate(int qnrRef, int copyFrom, string qnrRefString, string copyFromString);
    }
}

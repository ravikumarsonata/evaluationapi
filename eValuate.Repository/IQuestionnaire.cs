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
        Task questionnaireInsert(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<QuestionnaireHeader> getQnrList<QuestionnaireHeader>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<Rpt_Questionnaire> getQtnList<Rpt_Questionnaire>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        Task createFFQTemplate(int qnrRef, int copyFrom, string qnrRefString, string copyFromString);
        Task createMOTLayout(int qnrRef, int copyFrom, string qnrRefString, string copyFromString);
        int createNewTotalScoreMotText(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<MLToolLock_ChangeInfo> getMLToolLockChangeInfo<MLToolLock_ChangeInfo>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
    }
}

using Dapper;
using eValuat.Domain;
using eValuate.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.WebApi.Controllers
{
    [Route("api/Questionnaire")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaire _qtnRepository;

        public QuestionnaireController(IQuestionnaire qtnRepository)
        {
            _qtnRepository = qtnRepository;
        }
        [HttpGet]
        [Route("createQnr")]
        public async Task<string> CreateQuestionnaire()
        {
            //Questionnaire
            string qnrDesc = "New Questionnaire *";
            string includeCommonQtn = "N";
            string includeCommonRule = "N";
            int additionalQnr = -1;
            int additionalRule = -1;
            bool allowWebexpress = false;
            bool allowGapConnect = true;
            bool allowKodo = false;
            int clientCode = 1180; //It needs to change as a dynamic value
            string loggedinUser = "admin"; //It needs to change as a dynamic value

            var qnr_params = new DynamicParameters();
            qnr_params.Add("qnrDesc", qnrDesc, DbType.String);
            qnr_params.Add("incCommQtn", includeCommonQtn, DbType.String);
            qnr_params.Add("incCommRule", includeCommonRule, DbType.String);
            qnr_params.Add("addQnr", additionalQnr, DbType.Int32);
            qnr_params.Add("addRule", additionalRule, DbType.Int32);
            qnr_params.Add("allowWX", allowWebexpress, DbType.Boolean);
            qnr_params.Add("allowGC", allowGapConnect, DbType.Boolean);
            qnr_params.Add("allowKodo", allowKodo, DbType.Boolean);
            qnr_params.Add("clientCode", clientCode, DbType.Int32);
            qnr_params.Add("user", loggedinUser, DbType.String);
            //Insert the new Questionnaire
            var result = await Task.FromResult(_qtnRepository.createQuestionnaire<int>("[dbo].[QnrAuto_AddQnr]", qnr_params, commandType: CommandType.StoredProcedure));

            //Get Questionnaires
            var qnrList_params = new DynamicParameters();
            qnrList_params.Add("clientCode", clientCode, DbType.Int32);
            List<QuestionnaireHeader> ruxQnr = await Task.FromResult(_qtnRepository.getQnrList<QuestionnaireHeader>("[dbo].[GetQuestionnaire]", qnrList_params, commandType: CommandType.StoredProcedure));

            //Create FFQ Template
            int qnrRef = ruxQnr.Select(m => m.Qnr_Ref).FirstOrDefault();
            int sourceQnrRef = 99701; //This one is static in wm also.
            string qnrRefString = qnrRef + ",";
            string copyFromString = sourceQnrRef + ",";
            await _qtnRepository.createFFQTemplate(qnrRef,sourceQnrRef,qnrRefString,copyFromString);
            

            return "Questionnaire Created Successfully";
        }
    }
}

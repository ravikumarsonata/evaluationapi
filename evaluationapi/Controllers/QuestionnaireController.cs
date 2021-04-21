using Dapper;
using eValuate.Domain;
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
        //Questionnaire
        public string qnrDesc = "New Questionnaire *";
        public string includeCommonQtn = "N";
        public string includeCommonRule = "N";
        public int additionalQnr = -1;
        public int additionalRule = -1;
        public bool allowWebexpress = false;
        public bool allowGapConnect = true;
        public bool allowKodo = false;
        public int sourceQnrRef = 99701;
        //Question
        public int qtnRef;
        public int acqSeq;
        public string qtnCategory;
        public Single? potScore = 0;
        public string displayType;
        public string qtnName;
        public string qtnLabel;
        public string qtnText;
        public string qtnAltText;
        public string displayMask;
        public string commentRule;
        public string mapField;
        public int qtnImage;
        public string imageRule;
        public string qtnAltLabel;
        public string imageRuleExpr;
        public string commentRuleExpr;
        public bool overwrite;
        public bool gbwUseOnly;
        public bool includeInActPlan;
        public bool subQuestion;
        public string applicableRule;
        public int includeInReport;
        public int? matrix = null;
        public int? reportSeq = null;
        public static int matrixQtn = 1;
        public static int matrixHeader = 0;
        //createNewTotalScoreMotText 
        public string totalScoreMotText = "Total Score";
        public int staticTotalScoreId = 510300;
        public int qtnMotTextId;
        public bool includeInMOT = false;
        public string qtnMOTLabel;
        public string stdNumbering = "";

        public QuestionnaireController(IQuestionnaire qtnRepository)
        {
            _qtnRepository = qtnRepository;
        }
        [HttpGet]
        [Route("createQnr")]
        public async Task<ActionResult<string>> CreateQuestionnaire()
        {
            int clientCode = 1180; //It needs to change as a dynamic value
            string loggedinUser = "admin"; //It needs to change as a dynamic value

            //Create Questionnaire
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
            await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[QnrAuto_AddQnr]", qnr_params, commandType: CommandType.StoredProcedure));

            //Get Questionnaires
            var qnrList_params = new DynamicParameters();
            qnrList_params.Add("clientCode", clientCode, DbType.Int32);
            List<QuestionnaireHeader> ruxQnr = await Task.FromResult(_qtnRepository.getQnrList<QuestionnaireHeader>("[dbo].[eval_GetQuestionnaire]", qnrList_params, commandType: CommandType.StoredProcedure));
            int qnrRef = ruxQnr.Select(m => m.Qnr_Ref).FirstOrDefault();

            //Create FFQ Template
            string qnrRefString = qnrRef + ",";
            string copyFromString = sourceQnrRef + ",";
            await _qtnRepository.createFFQTemplate(qnrRef, sourceQnrRef, qnrRefString, copyFromString);

            //Create MOT Layout
            await _qtnRepository.createMOTLayout(qnrRef, sourceQnrRef, qnrRefString, copyFromString);

            //Get Questions
            var qtnList_params = new DynamicParameters();
            qtnList_params.Add("qnrRef", qnrRef, DbType.Int32);
            List<Rpt_Questionnaire> qtns = await Task.FromResult(_qtnRepository.getQnrList<Rpt_Questionnaire>("[dbo].[eval_GetQuestions]", qtnList_params, commandType: CommandType.StoredProcedure));

            //Create total score question
            if (qtns.Count == 0)
            {
                qtnRef = 9990000;
                acqSeq = 1000;
                qtnName = "total_score";
                qtnCategory = "SEC";
                qtnText = "Total Score";
                qtnAltText = "Total Score";
                potScore = null;

                var qtn_params = new DynamicParameters();
                qtn_params.Add("Qnr_Ref", qnrRef, DbType.Int32);
                qtn_params.Add("Qtn_Ref", qtnRef, DbType.Int32);
                qtn_params.Add("acq_seq", acqSeq, DbType.Int16);
                qtn_params.Add("Qtn_Category", qtnCategory, DbType.String);
                qtn_params.Add("Pot_Score", potScore, DbType.Double);
                qtn_params.Add("Display_Type", displayType, DbType.String);
                qtn_params.Add("Qtn_Name", qtnName, DbType.String);
                qtn_params.Add("Qtn_Label", qtnLabel, DbType.String);
                qtn_params.Add("Qtn_Text", qtnText, DbType.String);
                qtn_params.Add("Qtn_Alt_Text", qtnAltText, DbType.String);
                qtn_params.Add("Display_Mask", displayMask, DbType.String);
                qtn_params.Add("Comment_Rule", commentRule, DbType.String);
                qtn_params.Add("map_field", mapField, DbType.String);
                qtn_params.Add("qtn_image", qtnImage, DbType.Int32);
                qtn_params.Add("Image_Rule", imageRule, DbType.String);
                qtn_params.Add("Qtn_Alt_Label", qtnAltLabel, DbType.String);
                qtn_params.Add("Image_Rule_Expr", imageRuleExpr, DbType.String);
                qtn_params.Add("Comment_Rule_Expr", commentRuleExpr, DbType.String);
                qtn_params.Add("overwrite", overwrite ? 1 : 0, DbType.Boolean);
                qtn_params.Add("applicable_rule", applicableRule, DbType.String);
                qtn_params.Add("qtn_gbwuse", gbwUseOnly ? 1 : 0, DbType.Boolean);
                qtn_params.Add("includeInActPlan", includeInActPlan ? 1 : 0, DbType.Boolean);
                qtn_params.Add("qtn_sub", subQuestion ? 1 : 0, DbType.Boolean);
                qtn_params.Add("include_in_report", includeInReport, DbType.Boolean);
                qtn_params.Add("matrix", matrix == matrixQtn ? 1 : matrix == matrixHeader ? 0 : matrix, DbType.Boolean);
                qtn_params.Add("reportSeq", reportSeq, DbType.Int16);
                await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[WM_QC_updateQuestionnaire]", qtn_params, commandType: CommandType.StoredProcedure));

                //Create New Total Score Mot Text
                var tsText_params = new DynamicParameters();
                tsText_params.Add("mltext", totalScoreMotText, DbType.String);
                tsText_params.Add("textId", DbType.Int32, direction: ParameterDirection.Output);
                qtnMotTextId = await Task.FromResult(_qtnRepository.createNewTotalScoreMotText("[dbo].[eval_CreateMLText]", tsText_params, commandType: CommandType.StoredProcedure));

                //Update MOT Qtn TextId
                var motText_params = new DynamicParameters();
                motText_params.Add("qnr_ref", qnrRef, DbType.String);
                motText_params.Add("qtn_ref", qtnRef, DbType.String);
                motText_params.Add("textId", qtnMotTextId, DbType.Int32);
                motText_params.Add("includeInMOT", includeInMOT, DbType.String);
                motText_params.Add("qtnNumbering", qtnMOTLabel, DbType.String);
                await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[QnrAuto_updateMOTQtnTextId]", motText_params, commandType: CommandType.StoredProcedure));

                //Update MOT Std TextId
                var stdText_params = new DynamicParameters();
                stdText_params.Add("qnr_ref", qnrRef, DbType.String);
                stdText_params.Add("qtn_ref", qtnRef, DbType.String);
                stdText_params.Add("textId", qtnMotTextId, DbType.Int32);
                stdText_params.Add("includeInMOT", includeInMOT, DbType.String);
                stdText_params.Add("stdNumbering", stdNumbering, DbType.String);
                await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[QnrAuto_updateMOTStdTextId]", stdText_params, commandType: CommandType.StoredProcedure));
            }
            return Ok("Questionnaire Created Successfully");
        }

        [HttpPost]
        [Route("unlockQnr")]
        public async Task<ActionResult<string>> UnlockQuestionnaire([FromBody]Qnr_UnlockInfo qnr_UnlockInfo)
        {
            qnr_UnlockInfo.Qnr_Ref = 118034;
            qnr_UnlockInfo.CreatedDate = DateTime.Now;
            qnr_UnlockInfo.CreatedBy = "admin";
            qnr_UnlockInfo.Description = "eValuateUnlockTest";

            var lockInfo_params = new DynamicParameters();
            lockInfo_params.Add("qnr_ref", qnr_UnlockInfo.Qnr_Ref, DbType.Int32);
            lockInfo_params.Add("created_date", qnr_UnlockInfo.CreatedDate, DbType.DateTime);
            lockInfo_params.Add("created_by", qnr_UnlockInfo.CreatedBy, DbType.String);
            lockInfo_params.Add("change_desc", qnr_UnlockInfo.Description, DbType.String);
            await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[WM_QNRLock_unLockQnrRux]", lockInfo_params, commandType: CommandType.StoredProcedure));

            var mlToolLock_params = new DynamicParameters();
            mlToolLock_params.Add("qnr_ref", qnr_UnlockInfo.Qnr_Ref, DbType.Int32);
            List<MLToolLock_ChangeInfo> mlToolLockChangeInfo = await Task.FromResult(_qtnRepository.getMLToolLockChangeInfo<MLToolLock_ChangeInfo>("[dbo].[eval_GetQnrLockDetails]", mlToolLock_params, commandType: CommandType.StoredProcedure));

            if(mlToolLockChangeInfo.Count == 0)
            {
                var lockChangeInfo_params = new DynamicParameters();
                lockChangeInfo_params.Add("unlockedBy", qnr_UnlockInfo.CreatedBy, DbType.String);
                lockChangeInfo_params.Add("unlockedDesc", qnr_UnlockInfo.Description, DbType.String);
                lockChangeInfo_params.Add("qnrRef", qnr_UnlockInfo.Qnr_Ref, DbType.Int32);
                await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[MLToolLock_UnlockQNR]", lockChangeInfo_params, commandType: CommandType.StoredProcedure));
            }

            return Ok("Questionnaire Unlocked");
        }
        [HttpPost]
        [Route("lockQnr")]
        public async Task<ActionResult<string>> LockQuestionnaire([FromBody] Qnr_LockInfo qnr_LockInfo)
        {
            qnr_LockInfo.Qnr_Ref = 118034;
            qnr_LockInfo.Change_Id = 32230;
            qnr_LockInfo.Change_Desc = "eValuateLockTest";
            qnr_LockInfo.Approve_Date = DateTime.Now;
            qnr_LockInfo.Approve_By = "admin";
            qnr_LockInfo.Test_Date = DateTime.Now;
            qnr_LockInfo.Test_By = "admin";
            
            var lockInfo_params = new DynamicParameters();
            lockInfo_params.Add("change_id", qnr_LockInfo.Change_Id, DbType.Int64);
            lockInfo_params.Add("change_desc", qnr_LockInfo.Change_Desc, DbType.String);
            lockInfo_params.Add("approve_date", qnr_LockInfo.Approve_Date, DbType.DateTime);
            lockInfo_params.Add("approve_by", qnr_LockInfo.Approve_By, DbType.String);
            lockInfo_params.Add("test_date", qnr_LockInfo.Test_Date, DbType.DateTime);
            lockInfo_params.Add("test_by", qnr_LockInfo.Test_By, DbType.String);
            await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[WM_QNRLock_LockChangeID]", lockInfo_params, commandType: CommandType.StoredProcedure));

            var mlToolLock_params = new DynamicParameters();
            mlToolLock_params.Add("qnr_ref", qnr_LockInfo.Qnr_Ref, DbType.Int32);
            List<MLToolLock_ChangeInfo> mlToolLockChangeInfo = await Task.FromResult(_qtnRepository.getMLToolLockChangeInfo<MLToolLock_ChangeInfo>("[dbo].[eval_GetQnrLockDetails]", mlToolLock_params, commandType: CommandType.StoredProcedure));

            if (mlToolLockChangeInfo.Count > 0)
            {
                string desc = "";
                if (qnr_LockInfo.Change_Desc != mlToolLockChangeInfo.Select(m=>m.UnlockedDesc).FirstOrDefault())
                {
                    desc =  qnr_LockInfo.Change_Desc;
                    desc.Replace("'", "''");
                }
                var lockChangeInfo_params = new DynamicParameters();
                lockChangeInfo_params.Add("ChangeID", mlToolLockChangeInfo.Select(m=>m.ChangeID).FirstOrDefault(), DbType.Int64);
                lockChangeInfo_params.Add("LockedBy", qnr_LockInfo.Test_By, DbType.String);
                lockChangeInfo_params.Add("AdditionalInfo", desc, DbType.String);
                await Task.FromResult(_qtnRepository.questionnaireInsert("[dbo].[MLToolLock_LockQNR]", lockChangeInfo_params, commandType: CommandType.StoredProcedure));
            }

            return Ok("Questionnaire Locked");
        }
    }
}

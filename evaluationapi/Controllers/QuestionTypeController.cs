using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using eValuate.Domain;
using eValuate.Repository;

namespace evaluationapi.Controllers
{
    [Route("api/QuestionType")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypeRepositoryAsync _questionTypeRepository;

        public QuestionTypeController(IQuestionTypeRepositoryAsync questionTypeRepository)
        {
            _questionTypeRepository = questionTypeRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<List<QuestionType>>> GetAll()
        {
            var result = await Task.FromResult(_questionTypeRepository.GetAll<QuestionType>($"Select * From [dbo].[Question_Type]", null, commandType: CommandType.Text));
            return new JsonResult(result);
        }

        
    }
}

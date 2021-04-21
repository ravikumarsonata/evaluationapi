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
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypeRepositoryAsync _questionTypeRepository;

        public QuestionTypeController(IQuestionTypeRepositoryAsync questionTypeRepository)
        {
            _questionTypeRepository = questionTypeRepository;
        }

        [HttpGet(nameof(Get))]
        public async Task<ActionResult<List<QuestionType>>> Get()
        {
            var result = await Task.FromResult(_questionTypeRepository.GetAll<QuestionType>($"Select * from Question_Type Where IsActive = 1", null, commandType: CommandType.Text));
            return new JsonResult(result);
        }

        
    }
}

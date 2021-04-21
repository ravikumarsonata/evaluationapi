using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using eValuat.Domain;
using eValuate.Repository;
using Microsoft.Extensions.Logging;

namespace evaluationapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypeRepository _questionTypeRepository;
        private readonly ILogger _logger;

        public QuestionTypeController(IQuestionTypeRepository questionTypeRepository, ILogger logger)
        {
            _questionTypeRepository = questionTypeRepository;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {

            //return Ok(await _questionTypeRepository.GetAll());

            var questionTypes = await _questionTypeRepository.GetAll();

            if (questionTypes == null)
            {
                _logger.LogInformation("Record not found");
            }

            return Ok(questionTypes);
        }

        
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace evaluationapi.Controllers
{
    [Route("api/mysurvey")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private IWebHostEnvironment _hostEnvironment;

        public SurveyController(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> surveyList()
        {
            var jsonString = System.IO.File.ReadAllText(Path.Combine(_hostEnvironment.ContentRootPath, "mysurvey.json"));
            return Ok(jsonString);
        }
    }
}

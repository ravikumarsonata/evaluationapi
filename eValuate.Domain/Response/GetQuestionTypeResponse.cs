using eValuate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eValuate.Domain.Response
{
    public class GetQuestionTypeResponse
    {
        public List<QuestionType> QuestionTypes { get; set; }
    }
}

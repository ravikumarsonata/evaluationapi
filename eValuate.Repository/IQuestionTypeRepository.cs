using Dapper;
using eValuate.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Repository
{
    public interface IQuestionTypeRepository
    {
        Task<List<QuestionType>> GetAll();

    }
}

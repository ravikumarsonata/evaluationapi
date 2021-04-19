using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.WebApi.Models
{
    public class QuestionType
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionType_Id { get; set; }
        public string Response_Type_Text { get; set; }
        public string Response_Type_Value { get; set; }
    }
}

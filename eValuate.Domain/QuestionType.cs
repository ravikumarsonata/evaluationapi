using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Domain
{
    public class QuestionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Question_Type_Id { get; set; }
        public string Response_Type_Text { get; set; }
        public string Response_Type_Value { get; set; }
        public string Response_Type_Image { get; set; }
    }
}

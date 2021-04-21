using System;
using System.Collections.Generic;
using System.Text;

namespace eValuate.Domain
{
    public class Rpt_Questionnaire
    {
        public int Qnr_Ref { get; set; }
        public int Qtn_Ref { get; set; }
        public int Client_qtn_ref { get; set; }
        public int Section_Ref { get; set; }
        public float? Pot_Score { get; set; }
        public string Data_Type { get; set; }
        public string Display_Type { get; set; }
        public string Qtn_Text { get; set; }
        public string Qtn_Alt_Text { get; set; }
        public string Display_Mask { get; set; }
        public string Qtn_Label { get; set; }
        public string Qtn_Guideline { get; set; }
        public string Comment_Rule { get; set; }
        public Int16 Qtn_Seq { get; set; }
        public string Qtn_Category { get; set; }
        public string Qtn_Alt_Label { get; set; }
        public Int16 Include_In_Report { get; set; }
        public Int16 Page_Num { get; set; }
        public Int16 Include_In_GBR { get; set; }
        public string Qtn_Name { get; set; }
        public string map_field { get; set; }
        public Int16 acq_seq { get; set; }
        public string meal_qtns { get; set; }
        public Int64 Change_Id { get; set; }
        public Int64 row_id { get; set; }
        public Byte Qtn_Image { get; set; }
        public string Image_Rule { get; set; }
        public string image_rule_expr { get; set; }
        public string comment_rule_expr { get; set; }
        public int inheritQnr { get; set; }
        public bool overwrite { get; set; }
        public string applicable_rule { get; set; }
        public string notes { get; set; }
        public bool qtn_gbwuse { get; set; }
        public bool qtn_sub { get; set; }
        public bool Hide_QC { get; set; }
        public Byte matrix { get; set; }
        public DateTime date_created { get; set; }
        public bool IncludeInActPlan { get; set; }
    }
}

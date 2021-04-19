using System;

namespace eValuat.Domain
{
    public class QuestionnaireHeader
    {
        public int Qnr_Ref { get; set; }
        public string Qnr_Desc { get; set; }
        public string Total_Pot_Field { get; set; }
        public string Include_Common_Qtn { get; set; }
        public string Include_Common_Rule { get; set; }
        public int Additional_Qnr { get; set; }
        public int Additional_Rule { get; set; }
        public bool newQnr { get; set; }
        public Int64 Change_Id { get; set; }
        public Int64 row_id { get; set; }
        public byte scoring_type { get; set; }
        public int Survey_Qnr { get; set; }
        public byte aqc3_autowipcode { get; set; }
        public byte aqc3_ignoreComment { get; set; }
        public byte allow_webexpress { get; set; }
        public byte allow_gapconnect { get; set; }
        public byte allow_kodo { get; set; }
        public bool mainQnr { get; set; }
        public bool qtnattime { get; set; }
        public bool auto_validate_receipt { get; set; }
    }
}

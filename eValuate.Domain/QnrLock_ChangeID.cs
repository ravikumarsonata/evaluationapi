using System;
using System.Collections.Generic;
using System.Text;

namespace eValuate.Domain
{
    public class QnrLock_ChangeID
    {
        public Int64 Change_Id { get; set; }
        public string Change_Desc { get; set; }
        public DateTime Created_Date { get; set; }
        public string Created_By { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_By { get; set; }
        public DateTime Test_Date { get; set; }
        public string Test_By { get; set; }
        public byte Test_Mode { get; set; }
        public byte Current_Status { get; set; }
    }
}










using System;
using System.Collections.Generic;
using System.Text;

namespace eValuate.Domain
{
    public class Qnr_LockInfo
    {
        public int Qnr_Ref { get; set; }
        public int Change_Id { get; set; }
        public string Change_Desc { get; set; }
        public DateTime Approve_Date { get; set; }
        public string Approve_By { get; set; }
        public DateTime Test_Date { get; set; }
        public string Test_By { get; set; }
    }
}

		
	
	
	
	
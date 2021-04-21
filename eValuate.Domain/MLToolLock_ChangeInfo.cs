using System;
using System.Collections.Generic;
using System.Text;

namespace eValuate.Domain
{
    public class MLToolLock_ChangeInfo
    {
        public Int64 ChangeID { get; set; }
        public bool IsLocked { get; set; }
        public string UnlockedBy { get; set; }
        public string UnlockedDesc { get; set; }
        public DateTime UnlockedDate { get; set; }
        public string LockedBy { get; set; }
        public DateTime LockedDate { get; set; }
        public int QnrRef { get; set; }
        public string AdditionalInfo { get; set; }
    }
}







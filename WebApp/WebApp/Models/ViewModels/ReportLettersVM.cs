using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class ReportLettersVM
    {
        public string CorrectionDate { get; set; }
        public string ReceivedDate { get; set; }
        public string AuditingDate { get; set; }
        public string ResearchNumber { get; set; }
        public string ReceivedLetter { get; set; }
        public string CorrectionLetter { get; set; }
        public string AuditingLetter { get; set; }
        public string EditorialBoardTitle { get; set; }
        public string EditorialBoardHead { get; set; }
        public string JournalName { get; set; }
        public byte[] AdminSignature { get; set; }
        public byte[] EditorialHeadSignature { get; set; }
        public byte[] JournalStamp { get; set; }
      

    }
}

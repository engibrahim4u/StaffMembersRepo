using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public enum EmailCode
    {
        NewRegister=1,
        AdminCompleteDataAuthor,
        AdminOpenToCorrectionAuthor,
        AdminRecivedAuthor,
        AdminRecivedEditorialBoard,
        AdminRejectAdmin,
        AdminRejectAuthor,
        AdminSendToEditorialBoard,
        AssignEmail,
        AuditorAcceptAdmin,
        AuditorAcceptAfterModifyAdmin,
        AuditorAcceptAuditAdmin,
        AuditorApologAuditAdmin,
        AuditorModifyAndReviewAdmin,
        AuditorRejectAdmin,
        CreatedAuthor,
        EditorialBoardAcceptAuthor,
        EditorialBoardAcceptdmin,
        EditorialBoardRejectAuthor,
        EditorialBoardRejectdmin,
        EmailSentAdmin,
        ForgetPassword,
        SecretaryPayAuditor,
        SendAdmin,
        SendAuthor,
        StartAuditAdmin,
        StartAuditAuthor
    }
}

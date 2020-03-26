using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class ApprovalOrderApprover
    {
        //To Get all approvers for approval order 'drop-down list'
        public static List< CLayer.ApprovalOrderApprover> GetApprovalOrderApprover(int LoginID, int ApproverID)
        {
            DataLayer.ApprovalOrderApprover approvalOrderApprover = new DataLayer.ApprovalOrderApprover();
            return approvalOrderApprover.GetApprovalOrderApprover(LoginID, ApproverID);
        }

        //To get selected approver ID in drop-down list
        public static int GetApprovalOrderApproverID(long userId)
        {
            DataLayer.ApprovalOrderApprover approvalOrderApprover = new DataLayer.ApprovalOrderApprover();
            return approvalOrderApprover.GetApprovalOrderApproverID(userId);
        }
    }
}

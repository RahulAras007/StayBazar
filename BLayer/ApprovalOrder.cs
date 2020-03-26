using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class ApprovalOrder
    {
        public static void SaveApprovalOrder(CLayer.ApprovalOrder data)
        {
            DataLayer.ApprovalOrder approvalOrder = new DataLayer.ApprovalOrder();
            approvalOrder.SaveApprovalOrder(data);
        }

        //To delete present records from table'b2b_approvers'
        public static void DeleteB2bApproversRecords(long userID)
        {
            DataLayer.ApprovalOrder approvalOrder = new DataLayer.ApprovalOrder();
            approvalOrder.DeleteB2bApproversRecords(userID);
        }

        //To get selected approval order in drop-down list
        public static CLayer.Role.CorporateRoles GetApprovalOrder(long userId)
        {
            DataLayer.ApprovalOrder approvalOrder = new DataLayer.ApprovalOrder();
            return approvalOrder.GetApprovalOrder(userId);
        }

        //To get selected b2b_approver_id in b2b_approvers table
        public static int GetB2BApproverID(long userId)
        {
            DataLayer.ApprovalOrder approvalOrder = new DataLayer.ApprovalOrder();
            return approvalOrder.GetB2BApproverID(userId);
        }

        //To append already saved approver name and order into 'table': Edit click
        public static List<CLayer.ApprovalOrder> GetAssignedB2bApproversList(long userID)
        {
            DataLayer.ApprovalOrder approvalOrder = new DataLayer.ApprovalOrder();
            return approvalOrder.GetAssignedB2bApproversList(userID);
        }

        //To chnage `user` - `IsApprover` to false
        public static void SetIsApproverStatus(long userId)
        {
            DataLayer.ApprovalOrder approvalOrder = new DataLayer.ApprovalOrder();
            approvalOrder.SetIsApproverStatus(userId, false);
        }
    }
}

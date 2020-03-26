using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ApprovalOrderApprover : DataLink
    {
        //To Get all approvers for approval order 'drop-down list'
        public List<CLayer.ApprovalOrderApprover> GetApprovalOrderApprover(int LoginID, int ApproverID)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("PloginID", DataPlug.DataType._BigInt, LoginID));
            parameter.Add(Connection.GetParameter("PapproverID", DataPlug.DataType._BigInt, ApproverID));
            DataTable dataTable = Connection.GetTable("corporate_Approver_GetAll", parameter);
            List<CLayer.ApprovalOrderApprover> approvalOrderApprover = new List<CLayer.ApprovalOrderApprover>();
         
          
            foreach (DataRow dr in dataTable.Rows)
            {
                approvalOrderApprover.Add(new CLayer.ApprovalOrderApprover()
                {
                    UserID = Connection.ToInteger(dr["userid"]),
                    ApproverName = Connection.ToString(dr["ApproverName"])
                });
            }
            return approvalOrderApprover;
        }

        //To get selected approver ID in drop-down list
        public int GetApprovalOrderApproverID(long userId)
        {
            string sql = "SELECT approver_id FROM b2b_approvers tbl_b2bApprovers INNER JOIN `user` tbl_user ON tbl_b2bApprovers.approver_id = tbl_user.UserId WHERE tbl_b2bApprovers.user_id = " + userId.ToString();
            DataTable dataTable = Connection.GetSQLTable(sql);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return Connection.ToInteger(dataTable.Rows[0]["approver_id"]);
            }
            else
                return 0;
        }

    }
}

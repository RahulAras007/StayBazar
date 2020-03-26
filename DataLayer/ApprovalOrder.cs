using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class ApprovalOrder : DataLink
    {
        public void SaveApprovalOrder(CLayer.ApprovalOrder data)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("Puser_id", DataPlug.DataType._BigInt, data.user_id));
            parameter.Add(Connection.GetParameter("Papprover_id", DataPlug.DataType._BigInt, data.approver_id));
            parameter.Add(Connection.GetParameter("Papprover_order", DataPlug.DataType._Int, data.approver_order));
            parameter.Add(Connection.GetParameter("Pcreated_by", DataPlug.DataType._Int, data.created_by));
            Connection.ExecuteQueryScalar("corporate_ApprovalOrder_Save", parameter);
        }

        //To get selected approval order in drop-down list
        public CLayer.Role.CorporateRoles GetApprovalOrder(long userId)
        {
            string sql = "SELECT approver_order FROM b2b_approvers WHERE user_id=" + userId.ToString();
            DataTable dataTable = Connection.GetSQLTable(sql);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return (CLayer.Role.CorporateRoles)Connection.ToInteger(dataTable.Rows[0]["approver_order"]);
            }
            else
                return 0;
        }

        //To delete present record
        public void DeleteB2bApproversRecords(long userID)
        {
            string sql = "DELETE FROM b2b_approvers WHERE user_id=" + userID.ToString();
            Connection.ExecuteSqlQuery(sql);
        }



        //To get selected b2b_approver_id in b2b_approvers table
        public int GetB2BApproverID(long userId)
        {
            string sql = "SELECT b2b_approver_id FROM b2b_approvers WHERE user_id=" + userId.ToString();
            DataTable dataTable = Connection.GetSQLTable(sql);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return Connection.ToInteger(dataTable.Rows[0]["b2b_approver_id"]);
            }
            else
                return 0;
        }


        //To append already saved approver name and order into 'table': Edit click
        public List<CLayer.ApprovalOrder> GetAssignedB2bApproversList(long userID)
        {
            List<CLayer.ApprovalOrder> approvalOrder = new List<CLayer.ApprovalOrder>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("Puser_id", DataPlug.DataType._BigInt, userID));
            DataTable dataTable = Connection.GetTable("GetAssignedB2bApproversList", param);
            foreach (DataRow dr in dataTable.Rows)
            {
                approvalOrder.Add(new CLayer.ApprovalOrder()
                {
                    approver_id = Connection.ToLong(dr["approver_id"]),
                    ApproverName = Connection.ToString(dr["ApproverName"]),
                    approver_order = Connection.ToInteger(dr["approver_order"]),
                });
            }
            return approvalOrder;
        }

        //To chnage `user` - `IsApprover` to false
        public void SetIsApproverStatus(long userId, Boolean status)
        {
            Connection.ExecuteSqlQuery("UPDATE `user` SET `IsApprover`= (CASE WHEN((SELECT COUNT(`approver_id`) FROM `b2b_approvers` WHERE `approver_id`=" + userId.ToString()+") > 1) THEN 1 ELSE " + status + " END)  WHERE `userid`=" + userId.ToString());
            return;
        }
    }
}

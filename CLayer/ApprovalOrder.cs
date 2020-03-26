using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class ApprovalOrder
    {
        public long b2b_approver_id { get; set; }
        public long user_id { get; set; }
        public long approver_id { get; set; }
        public int approver_order { get; set; }
        public string ApproverName { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class BankAccount : ICommunication
    {
        public long BankAccountId { get; set; }
        public long UserId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchAddress { get; set; }
        public string RTGSNumber { get; set; }
        public string MICRCode { get; set; }

        public void Validate()
        {
            AccountName = Utils.CutString(AccountName, 150);
            AccountNumber = Utils.CutString(AccountNumber, 150);
            BankName = Utils.CutString(BankName, 100);
            BranchAddress = Utils.CutString(BranchAddress, 500);
            RTGSNumber = Utils.CutString(RTGSNumber, 150);
            MICRCode = Utils.CutString(MICRCode, 150);
        }
    }
}
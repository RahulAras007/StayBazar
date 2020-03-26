using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class BankAccount
    {
        public static CLayer.BankAccount GetOnUser(long UserId)
        {
            DataLayer.BankAccount bankaccount = new DataLayer.BankAccount();
            return bankaccount.GetOnUser(UserId);
        }

        public static long Save(CLayer.BankAccount data)
        {
            DataLayer.BankAccount bankaccount = new DataLayer.BankAccount();
            data.Validate();
            return bankaccount.Save(data);
        }
    }
}

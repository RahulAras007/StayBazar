using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class State
    {
        public static string GetGSTCode(long stateId)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetGSTCode(stateId);
        }



        public static List<CLayer.State> GetOnCountry(int countryId)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetOnCountry(countryId);
        }
        public static List<CLayer.State> GetAllState()
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetAllState();
        }

        public static CLayer.State Get(int stateId)
        {
            DataLayer.State state = new DataLayer.State();
            return state.Get(stateId);
        }

        public static int Save(CLayer.State data)
        {
            DataLayer.State state = new DataLayer.State();
            data.Validate();
            return state.Save(data);
        }
        public static List<CLayer.State> GetCustGstStateList(int Customerid, int Type)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetCustGstStateList(Customerid, Type);
        }

        public static CLayer.State GetStateID(string cityname)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetStateID(cityname);
        }
        public static CLayer.State GetStateID(string statename,long CountryID=0)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetStateID(statename,CountryID);
        }
        public static int GetBillingEntityStateID(long PropertyID)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetBillingEntityStateID(PropertyID);
        }
        public static int GetCustomerStateID(int CustomerID)
        {
            DataLayer.State state = new DataLayer.State();
            return state.GetCustomerStateID(CustomerID);
        }
        public static int UpdateGDSState(int CountryID, string State = "")
        {
            DataLayer.State state = new DataLayer.State();
            return state.UpdateGDSState(CountryID,State);
        }
    }
}

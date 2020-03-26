using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLayer
{
    public class Condition : ICommunication
    {
        public long ConditionId { get; set; }
        public long? AccommodationId { get; set; }
        public string ConditionText { get; set; }    
        public List<Condition> ConditionList { get; set; }

        //list
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long PropertyId { get; set; }

        public void Validate()
        {
            ConditionText = Utils.CutString(ConditionText, 250);       
        }

        public Condition()
        {
            ConditionList = new List<CLayer.Condition>();
        }
    }
}

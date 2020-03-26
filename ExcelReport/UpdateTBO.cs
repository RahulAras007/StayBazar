using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReport
{
    class UpdateTBO
    {
        public void UpdateMaster()
        {
            TBOFunctions TBO = new TBOFunctions();
            string lsz_TokenId = TBO.GetToken();

            if (lsz_TokenId != "")
            {
                TBO.GetCountry(lsz_TokenId);
                //TBO.GetHotelSearch(lsz_TokenId);
                //TBO.GetDestination(lsz_TokenId);
                //TBO.GetDestinationCityID();
            }

        }
        public void UpdateHotelCityExcel()
        {
            TBOFunctions TBO = new TBOFunctions();
            string lsz_TokenId = TBO.GetToken();

            if (lsz_TokenId != "")
            {
                //TBO.GetCountry(lsz_TokenId);
                //TBO.GetHotelSearch(lsz_TokenId);
                //TBO.GetDestination(lsz_TokenId);
                TBO.GetDestinationCityID();
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class ExcelImport
    {
        public static string ImportToDBFromExcel(DataTable dtSupplierDetails, DataTable dtPorpertyDetails, DataTable dtAccomodationDetails)
        {
            DataLayer.ExcelImport excelimport = new DataLayer.ExcelImport();
            return excelimport.ImportToDBFromExcel(dtSupplierDetails, dtPorpertyDetails, dtAccomodationDetails);
        }
        public static string GetTheCounts()
        {
            DataLayer.ExcelImport excelimport = new DataLayer.ExcelImport();
            return excelimport.GetTheCounts();
        }
        public static int ImportData()
        {
            DataLayer.ExcelImport excelimport = new DataLayer.ExcelImport();
            return excelimport.ImportData();
        }
    }
}

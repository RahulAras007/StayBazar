using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;


namespace ExcelReport
{
    class PropertyReport
    {
        public void GenerateReport()
        {
            //List<CLayer.PropertyDetailsReport> Reportlist;
            MySqlConnection _connection = null;
            try
            {
                string filename = System.Configuration.ConfigurationManager.AppSettings.Get("Filename");
                string filePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + filename;
                string destFolder = System.Configuration.ConfigurationManager.AppSettings.Get("DestFilePath");

               
                if(System.IO.File.Exists(filePath))
                {
                    try { 
                    File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {

                        LogHandler.LogError(ex);

                    }
                }
                try
                {
                    //getting connection here iteself for using data reader

                    _connection = new MySqlConnection();
                    _connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = _connection;
                    cmd.CommandText = "report_PropertyDetails_ExcelJob";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("pSearchString",MySqlDbType.VarChar).Value="";
                    cmd.Parameters.Add("pSearchvalue",MySqlDbType.Int32).Value=0;

                   MySqlDataReader dr = null;
                    CLayer.PropertyDetailsReport data = new CLayer.PropertyDetailsReport();
                    //No columns in report
                    const int NO_OF_COLUMNS = 31;

                    //open excel realted items

                    string xlsxFilePath = filePath;

                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                    {
                        //WriteExcelFile(ds, document);
                        //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
                        //  to a file, or writing to a MemoryStream.
                        document.AddWorkbookPart();
                        document.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                        //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
                        document.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

                        //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
                        WorkbookStylesPart workbookStylesPart = document.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
                        Stylesheet stylesheet = new Stylesheet();
                        workbookStylesPart.Stylesheet = stylesheet;

                      

                            //  For each worksheet you want to create
                            //string workSheetID = "rId1" ;
                            //string worksheetName = "ReportData";

                            WorksheetPart newWorksheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
                            newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                            // create sheet data
                            newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                            // save worksheet
                            //WriteDataTableToExcelWorksheet(dt, newWorksheetPart);
                            var worksheet = newWorksheetPart.Worksheet;
                            var sheetData = worksheet.GetFirstChild<SheetData>();

                          

                            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
                            //
                            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
                            //  cells of data, we'll know if to write Text values or Numeric cell values.

                            bool[] IsNumericColumn = new bool[NO_OF_COLUMNS];

                            string[] excelColumnNames = new string[NO_OF_COLUMNS];
                            for (int n = 0; n < NO_OF_COLUMNS; n++)
                                excelColumnNames[n] = CreateExcelFile.GetExcelColumnName(n);

                            //
                            //  Create the Header row in our Excel Worksheet
                            //
                            uint rowIndex = 1;

                            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                            sheetData.Append(headerRow);
                            string[] columnName = new string[NO_OF_COLUMNS] { 
                                "SupplierBusinessName" ,

                            "Login ID",
                            "Supplier EmailID",
                            "Supplier Contact Name",
                                "Supplier Address",
                                    "Supplier Phone",

                                    "Supplier Mobile",
                                    "Supplier City",
                                    "Supplier State",
                                    "Property Name",
                                    "Property Address",

                                    "B2CMarkup Short Term",
                                    "B2CMarkup Long Term",
                                    "B2BMarkup Short Term",
                                    "B2BMarkup Long Term",
                                    "Property City",

                                    "Property State",
                                    "Joined Date",
                                    "Accommodation Description",
                                    "Accommodation Type",
                                    "Quantity",

                                    "Daily Rate",
                                    "Weekly Rate",
                                    "Monthly Rate",
                                    "LongTerm Rate",
                                    "Guest Rate",

                                    "Bedrooms",
                                    "Supplier Total Accommodations",
                                    "Accommodation MaxPeople",
                                    "Adults",
                                    "Children"
                            };

                            for (int colInx = 0; colInx < NO_OF_COLUMNS; colInx++)
                            {
                                CreateExcelFile.AppendTextCell(excelColumnNames[colInx] + "1", columnName[colInx], headerRow);
                                IsNumericColumn[colInx] = false;
                            }
                           
                            try
                            {
                                //
                                //  Now, step through each row of data from DataReader
                                //
                                int colInx = 0;
                                //do not restart rowIndex, its value is already changed

                                _connection.Open();
                                dr = cmd.ExecuteReader(); //open reader

                                while (dr.Read())
                                {
                                    // create a new row, and append a set of this row's data to it.
                                    ++rowIndex;
                                    var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                                    sheetData.Append(newExcelRow);

                                        //numeric column - model
                                        // CreateExcelFile.AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                                        //text column - model 
                                        //CreateExcelFile.AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                                        colInx = 0;
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_Business_Name"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Login_ID"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_Email_ID"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_Contact_Name"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_Address"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_Phone"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["SupplierMobile"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_City"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Supplier_State"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Property_Name"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Property_Address"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["B2CMarkupShortTerm"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["B2CMarkupLongTerm"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["B2BMarkupShortTerm"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["B2BMarkupLongTerm"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Property_City"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Property_State"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToDate(dr["Joined_Date"]).ToString("dd/MM/yyyy"), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Accommodation_Description"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["AccommodationType"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Quantity"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["DailyRate"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["WeeklyRate"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["MonthlyRate"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["LongTermRate"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["GuestRate"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Bedrooms"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["SupplierTotalAccommodations"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Accommodation_MaxPeople"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Adults"]), newExcelRow);
                                        CreateExcelFile.AppendTextCell(excelColumnNames[colInx++] + rowIndex.ToString(), DataConv.ToString(dr["Children"]), newExcelRow);

                                }//while loop

                            }catch(Exception ex)
                            { LogHandler.LogError(ex); }

                            if (dr != null)
                            {
                                dr.Close(); //close reader
                            }
                            try
                            {
                                if (_connection.State == System.Data.ConnectionState.Open)
                                {
                                    _connection.Close();
                                }
                            }
                            catch { }
                            newWorksheetPart.Worksheet.Save();

                            // create the worksheet to workbook relation
                            document.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                            document.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                            {
                                Id = document.WorkbookPart.GetIdOfPart(newWorksheetPart),
                                SheetId = 1,
                                Name = "ReportData"
                            });

                         

                        document.WorkbookPart.Workbook.Save();
                    }


                   
                    
                }
                catch(Exception ex)
                {
                    LogHandler.LogError(ex);
                }finally
                {
                    try
                    {
                        if (_connection.State == System.Data.ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                    }
                    catch { }
                }
                File.Copy(filePath, destFolder + filename,true);

            }
            catch(Exception ex)
            {

                LogHandler.LogError(ex);

            }
        }
    }
}

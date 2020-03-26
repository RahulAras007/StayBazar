using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace ExcelReport
{
    public class ExcelCreator
    {
        //Usage

        //using DocumentFormat.OpenXml;
        //using DocumentFormat.OpenXml.Packaging;
        //using DocumentFormat.OpenXml.Spreadsheet;

        //string tplFile = null;
        //tplFile = Server.MapPath("~") + "Files\\custom_report.xlsx";

        //Dim filename As String = Server.MapPath("~") & "Temp\" & Guid.NewGuid.ToString() + ".xlsx"

        //System.IO.File.Copy(tplFile, filename);


        //WorkbookPart wbPart = null;
        //SpreadsheetDocument document = null;

        //document = SpreadsheetDocument.Open(filename, true);
        //wbPart = document.WorkbookPart();

        ////' SUMMARY SHEET

        //string sheetName = "Summary";

        //ExcelCreator.UpdateValue(ref wbPart, sheetName, "Q4", "Data shown for the timeframe of " + hidStartDate.Value + " from " + hidEndDate.Value, 0, true);
        //ExcelCreator.UpdateValue(ref wbPart, sheetName, "Q4", "data ", 0, true);


        //wbPart.Workbook.Save()
        //    document.Close()

        //Response.Buffer = True
        //        Response.ContentType = "application/vnd.ms-excel"
        //        Response.AppendHeader("content-disposition", "attachment; filename=CustomReport.xlsx")
        //        Response.Charset = ""
        //        Response.WriteFile(filename)
        //        Response.End()




        // Given a Worksheet and an address (like "AZ254"), either return a 
        // cell reference, or create the cell reference and return it.
        private static Cell InsertCellInWorksheet(ref Worksheet ws, string addressName)
        {
            SheetData sheetData = ws.GetFirstChild<SheetData>();
            Cell cell = null;

            UInt32 rowNumber = GetRowIndex(addressName);
            Row row = GetRow(sheetData, rowNumber);

            // If the cell you need already exists, return it.
            // If there is not a cell with the specified column name, insert one.  
            Cell refCell = row.Elements<Cell>().
                Where(c => c.CellReference.Value == addressName).FirstOrDefault();
            if (refCell != null)
            {
                cell = refCell;
            }
            else
            {
                cell = CreateCell(ref row, addressName);
            }
            return cell;
        }

        // Add a cell with the specified address to a row.
        private static Cell CreateCell(ref Row row, String address)
        {
            Cell cellResult;
            Cell refCell = null;

            // Cells must be in sequential order according to CellReference. 
            // Determine where to insert the new cell.
            foreach (Cell cell in row.Elements<Cell>())
            {
                if (string.Compare(cell.CellReference.Value, address, true) > 0)
                {
                    refCell = cell;
                    break;
                }
            }

            cellResult = new Cell();
            cellResult.CellReference = address;

            row.InsertBefore(cellResult, refCell);
            return cellResult;
        }

        // Return the row at the specified rowIndex located within
        // the sheet data passed in via wsData. If the row does not
        // exist, create it.
        private static Row GetRow(SheetData wsData, UInt32 rowIndex)
        {
            var row = wsData.Elements<Row>().
            Where(r => r.RowIndex.Value == rowIndex).FirstOrDefault();
            if (row == null)
            {
                row = new Row();
                row.RowIndex = rowIndex;
                wsData.Append(row);
            }
            return row;
        }

        // Given an Excel address such as E5 or AB128, GetRowIndex
        // parses the address and returns the row index.
        private static UInt32 GetRowIndex(string address)
        {
            string rowPart;
            UInt32 l;
            UInt32 result = 0;

            for (int i = 0; i < address.Length; i++)
            {
                if (UInt32.TryParse(address.Substring(i, 1), out l))
                {
                    rowPart = address.Substring(i, address.Length - i);
                    if (UInt32.TryParse(rowPart, out l))
                    {
                        result = l;
                        break;
                    }
                }
            }
            return result;
        }

        public static Sheet GetSheet(WorkbookPart wbPart, string sheetName)
        {
            return wbPart.Workbook.Descendants<Sheet>().Where(
                (s) => s.Name == sheetName).FirstOrDefault();
        }

        public static Worksheet GetWorksheet(WorkbookPart wbPart, Sheet sheet)
        {
            return ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
        }



        public static bool UpdateValue(ref WorkbookPart wbPart, Sheet sheet, Worksheet ws, string addressName, string value,
                              UInt32Value styleIndex, bool isString)
        {
            // Assume failure.
            bool updated = false;

            if (sheet != null)
            {
                Cell cell = InsertCellInWorksheet(ref ws, addressName);

                if (isString)
                {
                    // Either retrieve the index of an existing string,
                    // or insert the string into the shared string table
                    // and get the index of the new item.
                    int stringIndex = InsertSharedStringItem(ref wbPart, value);

                    cell.CellValue = new CellValue(stringIndex.ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                }
                else
                {
                    cell.CellValue = new CellValue(value);
                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                }

                if (styleIndex > 0)
                    cell.StyleIndex = styleIndex;

                // Save the worksheet.
                // ws.Save();
                updated = true;
            }

            return updated;
        }


        public static bool UpdateValue(ref WorkbookPart wbPart, string sheetName, string addressName, string value,
                               UInt32Value styleIndex, bool isString)
        {
            // Assume failure.
            bool updated = false;

            Sheet sheet = wbPart.Workbook.Descendants<Sheet>().Where(
                (s) => s.Name == sheetName).FirstOrDefault();

            if (sheet != null)
            {
                Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
                Cell cell = InsertCellInWorksheet(ref ws, addressName);

                if (isString)
                {
                    // Either retrieve the index of an existing string,
                    // or insert the string into the shared string table
                    // and get the index of the new item.
                    int stringIndex = InsertSharedStringItem(ref wbPart, value);

                    cell.CellValue = new CellValue(stringIndex.ToString());
                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                }
                else
                {
                    cell.CellValue = new CellValue(value);
                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                }

                if (styleIndex > 0)
                    cell.StyleIndex = styleIndex;

                // Save the worksheet.
                ws.Save();
                updated = true;
            }

            return updated;
        }

        // Given the main workbook part, and a text value, insert the text into 
        // the shared string table. Create the table if necessary. If the value 
        // already exists, return its index. If it doesn't exist, insert it and 
        // return its new index.
        private static int InsertSharedStringItem(ref WorkbookPart wbPart, string value)
        {
            int index = 0;
            bool found = false;
            var stringTablePart = wbPart
                .GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

            // If the shared string table is missing, something's wrong.
            // Just return the index that you found in the cell.
            // Otherwise, look up the correct text in the table.
            if (stringTablePart == null)
            {
                // Create it.
                stringTablePart = wbPart.AddNewPart<SharedStringTablePart>();
            }

            var stringTable = stringTablePart.SharedStringTable;
            if (stringTable == null)
            {
                stringTable = new SharedStringTable();
            }

            // Iterate through all the items in the SharedStringTable. 
            // If the text already exists, return its index.
            foreach (SharedStringItem item in stringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == value)
                {
                    found = true;
                    break;
                }
                index += 1;
            }

            if (!found)
            {
                stringTable.AppendChild(new SharedStringItem(new Text(value)));
                stringTable.Save();
            }

            return index;
        }


        public static string[] CalculateExcelColumnName(int NumberOfColumns)
        {
            string[] sa = new string[] {
		"A",
		"B",
		"C",
		"D",
		"E",
		"F",
		"G",
		"H",
		"I",
		"J",
		"K",
		"L",
		"M",
		"N",
		"O",
		"P",
		"Q",
		"R",
		"S",
		"T",
		"U",
		"V",
		"W",
		"X",
		"Y",
		"Z"
	};
            string[] result = new string[NumberOfColumns];
            string s = string.Empty;
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            i = InlineAssignHelper(ref j, InlineAssignHelper(ref k, -1));
            for (l = 0; l <= NumberOfColumns - 1; l++)
            {
                s = string.Empty;
                k += 1;
                if (k == 26)
                {
                    k = 0;
                    j += 1;
                    if (j == 26)
                    {
                        j = 0;
                        i += 1;
                    }
                }
                if (i >= 0)
                {
                    s += sa[i];
                }
                if (j >= 0)
                {
                    s += sa[j];
                }
                if (k >= 0)
                {
                    s += sa[k];
                }
                result[l] = s;
            }
            return result;
        }

        private static int InlineAssignHelper(ref int target, int value)
        {
            target = value;
            return value;
        }
    }
}
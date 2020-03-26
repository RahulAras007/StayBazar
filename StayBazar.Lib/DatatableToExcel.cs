
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace StayBazar.Lib
{
 
public class DataTableToExcel
{

    public static MemoryStream GetExcelStream(DataTable datTable, string sheetName, List<string> columnNames = null, List<int> customIndexes = null, List<CellValues> columnType = null)
    {
        MemoryStream strm = new MemoryStream();
        SpreadsheetDocument sheetDoc = SpreadsheetDocument.Create(strm, SpreadsheetDocumentType.Workbook);
        // Add a WorkbookPart to the document.
        //Dim sheetId As Integer = 1
        WorkbookPart workbookpart = default(WorkbookPart);

        WorksheetPart worksheetPart = default(WorksheetPart);
        Worksheet ws = default(Worksheet);
        SheetData shetData = default(SheetData);
        Sheets sheets = default(Sheets);

        Sheet sheet = default(Sheet);
        DocumentFormat.OpenXml.Spreadsheet.Row headerRow = default(DocumentFormat.OpenXml.Spreadsheet.Row);
        DocumentFormat.OpenXml.Spreadsheet.Cell cell = default(DocumentFormat.OpenXml.Spreadsheet.Cell);
        DocumentFormat.OpenXml.Spreadsheet.Row newRow = default(DocumentFormat.OpenXml.Spreadsheet.Row);

        workbookpart = sheetDoc.AddWorkbookPart();
        workbookpart.Workbook = new Workbook();
        workbookpart.Workbook.Sheets = new Sheets();

        worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
        shetData = new SheetData();
        ws = new Worksheet(shetData);
        worksheetPart.Worksheet = ws;
        sheets = sheetDoc.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
        //sheetDoc.WorkbookPart.Workbook.GetFirstChild(Of Spreadsheet.Sheets)()


        sheet = new Sheet();
        sheet.Id = sheetDoc.WorkbookPart.GetIdOfPart(worksheetPart);
        sheet.SheetId = 1;
        sheet.Name = sheetName;
        sheets.Append(sheet);

        headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
        //' Create Grid Header Row

        //If Not columnNames Is Nothing Then
        //    If columnNames.Count <> datTable.Columns.Count Then columnNames = Nothing
        //End If

        if (columnNames == null)
        {
            columnNames = new List<string>();
            foreach (System.Data.DataColumn column in datTable.Columns)
            {
                columnNames.Add(column.ColumnName.Replace("_", " "));
            }
        }

        foreach (string column in columnNames)
        {
            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column);
            headerRow.AppendChild(cell);
        }


        //Create DataRow
        if ((columnType != null))
        {
            if (datTable.Columns.Count != columnType.Count)
            {
                columnType = null;
            }
        }

        shetData.AppendChild(headerRow);

        if ((customIndexes != null))
        {
            int ix = 0;

            foreach (System.Data.DataRow dsrow in datTable.Rows)
            {
                newRow = new Row();
                ix = 0;
                foreach (int col in customIndexes)
                {
                    cell = new Cell();
                    if (columnType == null)
                    {
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    }
                    else
                    {
                        cell.DataType = columnType[ix];
                        ix = ix + 1;
                    }

                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                    newRow.AppendChild(cell);
                }
                shetData.AppendChild(newRow);
            }
        }
        else
        {
            int maxc = datTable.Columns.Count - 1;
            int ix = 0;

            foreach (System.Data.DataRow dsrow in datTable.Rows)
            {
                newRow = new Row();
                ix = 0;
                for (int col = 0; col <= maxc; col++)
                {
                    cell = new Cell();
                    if (columnType == null)
                    {
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    }
                    else
                    {
                        cell.DataType = columnType[ix];
                        ix = ix + 1;
                    }
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                    newRow.AppendChild(cell);
                }
                shetData.AppendChild(newRow);
            }
        }



        workbookpart.Workbook.Save();

        // Close the document.
        sheetDoc.Close();





        strm.Position = 0;
        return strm;
    }


    public static MemoryStream GetMultisheetExcelStream(List<DataTable> datTables, List<string> sheetNames, List<List<string>> lstColumnNames = null, List<List<int>> lstCustomIndexes = null, List<List<CellValues>> lstColumnType = null)
    {
        MemoryStream strm = new MemoryStream();
        SpreadsheetDocument sheetDoc = SpreadsheetDocument.Create(strm, SpreadsheetDocumentType.Workbook);
        // Add a WorkbookPart to the document.
        //Dim sheetId As Integer = 1
        WorkbookPart workbookpart = default(WorkbookPart);

        WorksheetPart worksheetPart = default(WorksheetPart);
        Worksheet ws = default(Worksheet);
        SheetData shetData = default(SheetData);
        Sheets sheets = default(Sheets);
        int sheetId = 1;
        Sheet sheet = default(Sheet);
        DocumentFormat.OpenXml.Spreadsheet.Row headerRow = default(DocumentFormat.OpenXml.Spreadsheet.Row);
        DocumentFormat.OpenXml.Spreadsheet.Cell cell = default(DocumentFormat.OpenXml.Spreadsheet.Cell);
        DocumentFormat.OpenXml.Spreadsheet.Row newRow = default(DocumentFormat.OpenXml.Spreadsheet.Row);

        workbookpart = sheetDoc.AddWorkbookPart();
        workbookpart.Workbook = new Workbook();
        workbookpart.Workbook.Sheets = new Sheets();

        worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
        shetData = new SheetData();
        ws = new Worksheet(shetData);
        worksheetPart.Worksheet = ws;
        sheets = sheetDoc.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
        //sheetDoc.WorkbookPart.Workbook.GetFirstChild(Of Spreadsheet.Sheets)()




        DataTable datTable = default(DataTable);

        List<string> columnNames = default(List<string>);
        List<int> customIndexes = default(List<int>);
        List<CellValues> columnType = default(List<CellValues>);
        int idx = 0;

        foreach (string SheetName in sheetNames)
        {
            //Get data from list

            datTable = datTables[idx];
            if (lstCustomIndexes == null)
            {
                customIndexes = null;
            }
            else
            {
                customIndexes = lstCustomIndexes[idx];
            }
            if (lstColumnNames == null)
            {
                columnNames = null;
            }
            else
            {
                columnNames = lstColumnNames[idx];
            }
            if (lstColumnType == null)
            {
                columnType = null;
            }
            else
            {
                columnType = lstColumnType[idx];
            }
            idx = idx + 1;

            //prepare excel sheet
            if (idx > 0)
            {
                worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                shetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(shetData);
            }
            sheet = new Sheet();
            sheet.Id = sheetDoc.WorkbookPart.GetIdOfPart(worksheetPart);
            sheet.SheetId = (UInt32) sheetId;
            sheetId = sheetId + 1;
            sheet.Name = SheetName;
            sheets.Append(sheet);

            headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            //' Create Grid Header Row

            //If Not columnNames Is Nothing Then
            //    If columnNames.Count <> datTable.Columns.Count Then columnNames = Nothing
            //End If

            if (columnNames == null)
            {
                columnNames = new List<string>();
                foreach (System.Data.DataColumn column in datTable.Columns)
                {
                    columnNames.Add(column.ColumnName.Replace("_", " "));
                }
            }

            foreach (string column in columnNames)
            {
                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column);
                headerRow.AppendChild(cell);
            }


            //Create DataRow
            if ((columnType != null))
            {
                if (datTable.Columns.Count != columnType.Count)
                {
                    columnType = null;
                }
            }

            shetData.AppendChild(headerRow);

            if ((customIndexes != null))
            {
                int ix = 0;

                foreach (System.Data.DataRow dsrow in datTable.Rows)
                {
                    newRow = new Row();
                    ix = 0;
                    foreach (int col in customIndexes)
                    {
                        cell = new Cell();
                        if (columnType == null)
                        {
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        }
                        else
                        {
                            cell.DataType = columnType[ix];
                            ix = ix + 1;
                        }

                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }
                    shetData.AppendChild(newRow);
                }
            }
            else
            {
                int maxc = datTable.Columns.Count - 1;
                int ix = 0;

                foreach (System.Data.DataRow dsrow in datTable.Rows)
                {
                    newRow = new Row();
                    ix = 0;
                    for (int col = 0; col <= maxc; col++)
                    {
                        cell = new Cell();
                        if (columnType == null)
                        {
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        }
                        else
                        {
                            cell.DataType = columnType[ix];
                            ix = ix + 1;
                        }
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }
                    shetData.AppendChild(newRow);
                }
            }





        }
        //end of looping







        workbookpart.Workbook.Save();

        // Close the document.
        sheetDoc.Close();





        strm.Position = 0;
        return strm;
    }

}

}




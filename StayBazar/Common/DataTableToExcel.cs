using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Collections.Generic;
using System.Data;

public class DataTableToExcel
{

    public const string CONTENT_TYPE_XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    //Styles
    private const uint STYLE_BOLD_CENTERED = 1;
    private const uint STYLE_CENTERED = 2;

    public static MemoryStream GetExcelStream(DataTable datTable, string sheetName,bool hasHeaderRow = true, List<string> columnNames = null, List<int> customIndexes = null, List<CellValues> columnType = null)
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
        sheets = sheetDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>();
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
        if (hasHeaderRow)
        {
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

            shetData.AppendChild(headerRow);
        }

        //Create DataRow
        if (!ReferenceEquals(columnType, null))
        {
            if (datTable.Columns.Count != columnType.Count)
            {
                columnType = null;
            }
        }

        

        if (!ReferenceEquals(customIndexes, null))
        {
            int ix = 0;

            foreach (System.Data.DataRow dsrow in datTable.Rows)
            {
                newRow = new Row();
                ix = 0;
                foreach (int col in customIndexes)
                {
                    cell = new Cell();
                    if (ReferenceEquals(columnType, null))
                    {
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    }
                    else
                    {
                        cell.DataType = columnType[ix];
                        ix++;
                    }

                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                    newRow.AppendChild(cell);
                }
                shetData.AppendChild(newRow);
            }
        }
        else
        {
            int maxc = System.Convert.ToInt32(datTable.Columns.Count - 1);
            int ix = 0;
            
            if(columnType == null)
            {
                columnType = new List<CellValues>();
                foreach(DataColumn cm in datTable.Columns)
                {
                   if(cm.DataType == typeof(decimal) || cm.DataType == typeof(double) || cm.DataType == typeof(int) || cm.DataType == typeof(long))
                    {
                        columnType.Add(CellValues.Number);
                    }
                    //else if (cm.DataType == System.Type.GetType("System.DateTime")) // has some issues better to avoid
                    //{
                    //    columnType.Add(CellValues.Date);
                    //}
                    else
                    {
                        columnType.Add(CellValues.String);
                    }
                }
            }

            foreach (System.Data.DataRow dsrow in datTable.Rows)
            {
                newRow = new Row();
                ix = 0;
                for (var col = 0; col <= maxc; col++)
                {
                    cell = new Cell();
                    if (ReferenceEquals(columnType, null))
                    {
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    }
                    else
                    {
                        cell.DataType = columnType[ix];
                        ix++;
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

    // ''' <summary>
    // ''' Generates excel sheet on stream
    // ''' </summary>
    // ''' <param name="datTables">List of datatables to pass</param>
    // ''' <param name="sheetNames">Sheetnames to use for each datatables</param>
    // ''' <param name="lstColumnNames">Custom Column Header names.If used, specify column headers of all datatables, or specify upto needed datatable. For example if custom headers are needed for 3rd datatable- specify for 1st and 2nd too</param>
    // ''' <param name="lstCustomIndexes">Custom Columns, use when all the datatable columns are not needed.Usage is same like Custom Column Names</param>
    // ''' <param name="lstColumnType">Specify Column is string or number. it can automatically detect </param>
    // ''' <param name="makeHeaderRowBold">Header row should be bold and centered</param>
    // ''' <param name="alignCenterColumnIndexesZeroBased">Pass the indexes to be centered, usage like column names. For each datatable pass only required indices</param>
    // ''' <returns></returns>
    // ''' <remarks></remarks>
    //Public Shared Function GetMultisheetExcelStream(datTables As List(Of DataTable), sheetNames As List(Of String), Optional lstColumnNames As List(Of List(Of String)) = Nothing, Optional lstCustomIndexes As List(Of List(Of Integer)) = Nothing, Optional lstColumnType As List(Of List(Of CellValues)) = Nothing, Optional makeHeaderRowBold As Boolean = True, Optional alignCenterColumnIndexesZeroBased As List(Of List(Of Integer)) = Nothing) As MemoryStream
    //    Dim strm As New MemoryStream
    //    Dim sheetDoc As SpreadsheetDocument = SpreadsheetDocument.Create(strm, SpreadsheetDocumentType.Workbook)
    //    ' Add a WorkbookPart to the document.
    //    'Dim sheetId As Integer = 1
    //    Dim workbookpart As WorkbookPart

    //    Dim worksheetPart As WorksheetPart
    //    Dim ws As Worksheet
    //    Dim shetData As SheetData
    //    Dim sheets As Sheets
    //    Dim sheetId As Integer = 1
    //    Dim sheet As Sheet
    //    Dim headerRow As DocumentFormat.OpenXml.Spreadsheet.Row
    //    Dim cell As DocumentFormat.OpenXml.Spreadsheet.Cell
    //    Dim newRow As DocumentFormat.OpenXml.Spreadsheet.Row

    //    workbookpart = sheetDoc.AddWorkbookPart
    //    workbookpart.Workbook = New Workbook

    //    AddStyleSheet(sheetDoc)

    //    workbookpart.Workbook.Sheets = New Sheets()

    //    worksheetPart = workbookpart.AddNewPart(Of WorksheetPart)()
    //    shetData = New SheetData
    //    ws = New Worksheet(shetData)
    //    worksheetPart.Worksheet = ws
    //    sheets = sheetDoc.WorkbookPart.Workbook.GetFirstChild(Of Spreadsheet.Sheets)()
    //    'sheetDoc.WorkbookPart.Workbook.GetFirstChild(Of Spreadsheet.Sheets)()




    //    Dim datTable As DataTable

    //    Dim columnNames As List(Of String) = Nothing
    //    Dim customIndexes As List(Of Integer) = Nothing
    //    Dim columnType As List(Of CellValues) = Nothing
    //    Dim alignCenterColumns As List(Of Integer) = Nothing
    //    Dim idx As Integer = 0
    //    For Each SheetName As String In sheetNames

    //        'Get data from list

    //        datTable = datTables(idx)
    //        If lstCustomIndexes Is Nothing Then
    //            customIndexes = Nothing
    //        Else
    //            If lstCustomIndexes.Count > idx Then
    //                customIndexes = lstCustomIndexes(idx)
    //            End If
    //        End If

    //        If lstColumnNames Is Nothing Then
    //            columnNames = Nothing
    //        Else
    //            If lstColumnNames.Count > idx Then
    //                columnNames = lstColumnNames(idx)
    //            End If
    //        End If

    //        If lstColumnType Is Nothing Then
    //            columnType = Nothing
    //        Else
    //            If lstColumnType.Count > idx Then
    //                columnType = lstColumnType(idx)
    //            End If
    //        End If

    //            alignCenterColumns = Nothing
    //            If Not (alignCenterColumnIndexesZeroBased Is Nothing) Then
    //                If alignCenterColumnIndexesZeroBased.Count > idx Then
    //                    alignCenterColumns = alignCenterColumnIndexesZeroBased(idx)
    //                End If
    //            End If

    //            idx = idx + 1

    //            'prepare excel sheet
    //            If idx > 0 Then
    //                worksheetPart = workbookpart.AddNewPart(Of WorksheetPart)()
    //                shetData = New SheetData
    //                worksheetPart.Worksheet = New Worksheet(shetData)
    //            End If
    //            sheet = New Sheet
    //            sheet.Id = sheetDoc.WorkbookPart.GetIdOfPart(worksheetPart)
    //            sheet.SheetId = sheetId
    //            sheetId = sheetId + 1
    //            sheet.Name = SheetName
    //            sheets.Append(sheet)

    //            headerRow = New DocumentFormat.OpenXml.Spreadsheet.Row()
    //            '' Create Grid Header Row

    //            'If Not columnNames Is Nothing Then
    //            '    If columnNames.Count <> datTable.Columns.Count Then columnNames = Nothing
    //            'End If

    //            If columnNames Is Nothing Then
    //                columnNames = New List(Of String)()
    //                Dim clt As Boolean = False
    //                If columnType Is Nothing Then
    //                    columnType = New List(Of CellValues)
    //                    clt = True
    //                End If
    //                For Each column As System.Data.DataColumn In datTable.Columns
    //                    columnNames.Add(column.ColumnName.Replace("_", " "))
    //                    If clt Then
    //                        columnType.Add(GetColumnType(column.DataType))
    //                    End If
    //                Next
    //            End If
    //            '' top row columns
    //            For Each column As String In columnNames
    //                cell = New DocumentFormat.OpenXml.Spreadsheet.Cell()
    //                If makeHeaderRowBold Then
    //                    cell.StyleIndex = STYLE_BOLD_CENTERED ' Convert.ToUInt32(1)
    //                End If
    //                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
    //                cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(column)
    //                headerRow.AppendChild(cell)
    //            Next

    //            'If columnNames Is Nothing Then
    //            '    columnNames = New List(Of String)()
    //            '    For Each column As System.Data.DataColumn In datTable.Columns
    //            '        '  columnNames.Add(column.ColumnName.Replace("_", " "))
    //            '        cell = New DocumentFormat.OpenXml.Spreadsheet.Cell()
    //            '        cell.DataType = GetColumnType(column.DataType)
    //            '        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName.Replace("_", " "))
    //            '        headerRow.AppendChild(cell)
    //            '    Next
    //            'Else
    //            '    For Each column As String In columnNames
    //            '        cell = New DocumentFormat.OpenXml.Spreadsheet.Cell()
    //            '        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
    //            '        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(column)
    //            '        headerRow.AppendChild(cell)
    //            '    Next
    //            'End If




    //            'Create DataRow
    //            If Not columnType Is Nothing Then
    //                If datTable.Columns.Count <> columnType.Count Then
    //                    columnType = Nothing
    //                End If
    //            End If

    //            shetData.AppendChild(headerRow)

    //            If Not customIndexes Is Nothing Then
    //                Dim ix As Integer = 0

    //                For Each dsrow As System.Data.DataRow In datTable.Rows
    //                    newRow = New Row()
    //                    ix = 0 'col index
    //                    For Each col As Integer In customIndexes
    //                        cell = New Cell()

    //                        If columnType Is Nothing Then
    //                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
    //                        Else
    //                            cell.DataType = columnType(ix)
    //                        End If
    //                        If Not (alignCenterColumns Is Nothing) Then
    //                            If alignCenterColumns.Contains(ix) Then
    //                                cell.StyleIndex = STYLE_CENTERED
    //                            End If
    //                        End If
    //                        ix = ix + 1 'increment col index
    //                        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow(col).ToString())
    //                        newRow.AppendChild(cell)
    //                    Next
    //                    shetData.AppendChild(newRow)
    //                Next
    //            Else
    //                Dim maxc As Integer = datTable.Columns.Count - 1
    //                Dim ix As Integer = 0

    //                For Each dsrow As System.Data.DataRow In datTable.Rows
    //                    newRow = New Row()
    //                    ix = 0
    //                    For col = 0 To maxc
    //                        cell = New Cell()
    //                        If columnType Is Nothing Then
    //                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
    //                        Else
    //                            cell.DataType = columnType(ix)
    //                        End If
    //                        If Not (alignCenterColumns Is Nothing) Then
    //                            If alignCenterColumns.Contains(ix) Then
    //                                cell.StyleIndex = STYLE_CENTERED
    //                            End If
    //                        End If
    //                        ix = ix + 1 'increment col index
    //                        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow(col).ToString())
    //                        newRow.AppendChild(cell)
    //                    Next
    //                    shetData.AppendChild(newRow)
    //                Next
    //            End If


    //            workbookpart.Workbook.Save()
    //    Next 'end of looping


    //    ' workbookpart.Workbook.Save()

    //    ' Close the document.
    //    sheetDoc.Close()


    //    strm.Position = 0
    //    Return strm
    //End Function

    /// <summary>
    /// Generates excel sheet on stream
    /// </summary>
    /// <param name="datTables">List of datatables to pass</param>
    /// <param name="sheetNames">Sheetnames to use for each datatables</param>
    /// <param name="lstColumnNames">Custom Column Header names.If used, specify column headers of all datatables, or specify upto needed datatable. For example if custom headers are needed for 3rd datatable- specify for 1st and 2nd too</param>
    /// <param name="lstCustomIndexes">Custom Columns, use when all the datatable columns are not needed.Usage is same like Custom Column Names</param>
    /// <param name="lstColumnType">Specify Column is string or number. it can automatically detect </param>
    /// <param name="makeHeaderRowBold">Header row should be bold and centered</param>
    /// <param name="alignCenterColumnIndexesZeroBased">Pass the indexes to be centered, usage like column names. For each datatable pass only required indices</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static MemoryStream GetMultisheetExcelStream(List<DataTable> datTables, List<string> sheetNames, List<List<string>> lstColumnNames = null, List<List<int>> lstCustomIndexes = null, List<List<CellValues>> lstColumnType = null, bool makeHeaderRowBold = true, List<List<int>> alignCenterColumnIndexesZeroBased = null, List<List<List<string>>> dataBeforeEachTable = null, List<List<List<string>>> dataAfterEachTable = null)
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
        UInt32Value sheetId = 1;
        Sheet sheet = default(Sheet);
        DocumentFormat.OpenXml.Spreadsheet.Row headerRow = default(DocumentFormat.OpenXml.Spreadsheet.Row);
        DocumentFormat.OpenXml.Spreadsheet.Cell cell = default(DocumentFormat.OpenXml.Spreadsheet.Cell);
        DocumentFormat.OpenXml.Spreadsheet.Row newRow = default(DocumentFormat.OpenXml.Spreadsheet.Row);

        workbookpart = sheetDoc.AddWorkbookPart();
        workbookpart.Workbook = new Workbook();

        AddStyleSheet(sheetDoc);

        workbookpart.Workbook.Sheets = new Sheets();

        worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
        shetData = new SheetData();
        ws = new Worksheet(shetData);
        worksheetPart.Worksheet = ws;
        sheets = sheetDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>();
        //sheetDoc.WorkbookPart.Workbook.GetFirstChild(Of Spreadsheet.Sheets)()




        DataTable datTable = default(DataTable);

        List<string> columnNames = null;
        List<int> customIndexes = null;
        List<CellValues> columnType = null;
        List<int> alignCenterColumns = null;
        List<List<string>> dataAfterTable = null;
        List<List<string>> dataBeforeTable = null;

        bool columnNamesNotProvided = true;
        bool columnTypeNotProvided = true;
        int idx = 0;

        foreach (string SheetName in sheetNames)
        {
            //Get data from list

            datTable = datTables[idx];
            customIndexes = null;
            if ((lstCustomIndexes != null))
            {
                if (lstCustomIndexes.Count > idx)
                {
                    customIndexes = lstCustomIndexes[idx];
                }
            }

            columnNames = null;
            columnNamesNotProvided = true;

            if ((lstColumnNames != null))
            {
                if (lstColumnNames.Count > idx)
                {
                    columnNames = lstColumnNames[idx];
                    if (columnNames != null) { columnNamesNotProvided = false; }
                }
            }

            columnType = null;
            columnTypeNotProvided = true;
            if ((lstColumnType != null))
            {
                if (lstColumnType.Count > idx)
                {
                    columnType = lstColumnType[idx];
                    if (columnType != null) { columnTypeNotProvided = false; }
                }
            }

            alignCenterColumns = null;
            if ((alignCenterColumnIndexesZeroBased != null))
            {
                if (alignCenterColumnIndexesZeroBased.Count > idx)
                {
                    alignCenterColumns = alignCenterColumnIndexesZeroBased[idx];
                }
            }


            dataAfterTable = null;
            if ((dataAfterEachTable != null) && dataAfterEachTable.Count > idx)
            {
                dataAfterTable = dataAfterEachTable[idx];
            }
            dataBeforeTable = null;
            if ((dataBeforeEachTable != null) && dataBeforeEachTable.Count > idx)
            {
                dataBeforeTable = dataBeforeEachTable[idx];
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
            sheet.SheetId = sheetId;
            sheetId = sheetId + 1;
            sheet.Name = SheetName;
            sheets.Append(sheet);

            // Append data before table - above heading
            if ((dataBeforeTable != null))
            {
                //'append data before table
                int ix = 0;
                foreach (List<string> cusRow in dataBeforeTable)
                {
                    if ((cusRow != null) && cusRow.Count > 0)
                    {
                        newRow = new Row();
                        ix = 0;
                        //col index
                        foreach (string col in cusRow)
                        {
                            cell = new Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            //If Not (alignCenterColumns Is Nothing) Then
                            //    If alignCenterColumns.Contains(ix) Then
                            //        cell.StyleIndex = STYLE_CENTERED
                            //    End If
                            //End If
                            ix = ix + 1;
                            //increment col index
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(col);
                            newRow.AppendChild(cell);
                        }
                        shetData.AppendChild(newRow);
                    }
                }

            }

            //if ((dataBeforeTable != null))
            //{
            //    //'append data before table
            //    int ix = 0;
            //    foreach (List<string> cusRow in dataBeforeTable)
            //    {
            //        if ((cusRow != null) && cusRow.Count > 0)
            //        {
            //            newRow = new Row();
            //            ix = 0;
            //            //col index
            //            foreach (string col in cusRow)
            //            {
            //                cell = new Cell();
            //                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            //                //If Not (alignCenterColumns Is Nothing) Then
            //                //    If alignCenterColumns.Contains(ix) Then
            //                //        cell.StyleIndex = STYLE_CENTERED
            //                //    End If
            //                //End If
            //                ix = ix + 1;
            //                //increment col index
            //                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(col);
            //                newRow.AppendChild(cell);
            //            }
            //            shetData.AppendChild(newRow);
            //        }
            //    }

            //}


            headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
            //' Create Grid Header Row

            //If Not columnNames Is Nothing Then
            //    If columnNames.Count <> datTable.Columns.Count Then columnNames = Nothing
            //End If

            if (columnNames == null)
            {
                columnNames = new List<string>();
                bool clt = false;
                if (columnType == null)
                {
                    columnType = new List<CellValues>();
                    clt = true;
                }
                foreach (System.Data.DataColumn column in datTable.Columns)
                {
                    columnNames.Add(column.ColumnName.Replace("_", " "));
                    if (clt)
                    {
                        columnType.Add(GetColumnType(column.DataType));
                    }
                }
            }
            //' top row columns
            if (customIndexes != null && columnNamesNotProvided)
            {

                foreach (int clmn in customIndexes)
                {
                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    if (makeHeaderRowBold)
                    {
                        cell.StyleIndex = STYLE_BOLD_CENTERED;
                        // Convert.ToUInt32(1)
                    }
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(columnNames[clmn]);
                    headerRow.AppendChild(cell);
                }
            }
            else
            {
                foreach (string column in columnNames)
                {
                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    if (makeHeaderRowBold)
                    {
                        cell.StyleIndex = STYLE_BOLD_CENTERED;
                        // Convert.ToUInt32(1)
                    }
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column);
                    headerRow.AppendChild(cell);
                }
            }
            shetData.AppendChild(headerRow);

            //If columnNames Is Nothing Then
            //    columnNames = New List(Of String)()
            //    For Each column As System.Data.DataColumn In datTable.Columns
            //        '  columnNames.Add(column.ColumnName.Replace("_", " "))
            //        cell = New DocumentFormat.OpenXml.Spreadsheet.Cell()
            //        cell.DataType = GetColumnType(column.DataType)
            //        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName.Replace("_", " "))
            //        headerRow.AppendChild(cell)
            //    Next
            //Else
            //    For Each column As String In columnNames
            //        cell = New DocumentFormat.OpenXml.Spreadsheet.Cell()
            //        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
            //        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(column)
            //        headerRow.AppendChild(cell)
            //    Next
            //End If




            //Create DataRow
            if ((columnType != null))
            {
                if (customIndexes == null & datTable.Columns.Count != columnType.Count)
                {
                    columnType = null;
                }
                else if (customIndexes != null && customIndexes.Count != columnType.Count)
                {
                    columnType = null;
                }
            }



            

            if (customIndexes != null)
            {
                int ix = 0;

                foreach (System.Data.DataRow dsrow in datTable.Rows)
                {
                    newRow = new Row();
                    ix = 0;
                    //col index
                    foreach (int col in customIndexes)
                    {
                        cell = new Cell();

                        if (columnType == null)
                        {
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        }
                        else
                        {
                            if (columnTypeNotProvided)
                                cell.DataType = columnType[col];
                            else
                                cell.DataType = columnType[ix];
                        }
                        if ((alignCenterColumns != null))
                        {
                            if (alignCenterColumns.Contains(col))
                            {
                                cell.StyleIndex = STYLE_CENTERED;
                            }
                        }
                        ix = ix + 1;
                        //increment col index
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
                    for (var col = 0; col <= maxc; col++)
                    {
                        cell = new Cell();
                        if (columnType == null)
                        {
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        }
                        else
                        {
                            cell.DataType = columnType[ix];
                        }
                        if ((alignCenterColumns != null))
                        {
                            if (alignCenterColumns.Contains(ix))
                            {
                                cell.StyleIndex = STYLE_CENTERED;
                            }
                        }
                        ix = ix + 1;
                        //increment col index
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }
                    shetData.AppendChild(newRow);
                }
            }


            if ((dataAfterTable != null))
            {
                //'append data after table
                int ix = 0;
                foreach (List<string> cusRow in dataAfterTable)
                {
                    if ((cusRow != null) && cusRow.Count > 0)
                    {
                        newRow = new Row();
                        ix = 0;
                        //col index
                        foreach (string col in cusRow)
                        {
                            cell = new Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            //If Not (alignCenterColumns Is Nothing) Then
                            //    If alignCenterColumns.Contains(ix) Then
                            //        cell.StyleIndex = STYLE_CENTERED
                            //    End If
                            //End If
                            ix = ix + 1;
                            //increment col index
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(col);
                            newRow.AppendChild(cell);
                        }
                        shetData.AppendChild(newRow);
                    }
                }

            }

            workbookpart.Workbook.Save();
        }
        //end of looping


        // workbookpart.Workbook.Save()

        // Close the document.
        sheetDoc.Close();


        strm.Position = 0;
        return strm;
    }
    private static WorkbookStylesPart AddStyleSheet(SpreadsheetDocument spreadsheet)
    {

        WorkbookStylesPart stylesheet = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();

        Stylesheet workbookstylesheet = new Stylesheet();

        Font font0 = new Font(); // Default font



        Font font1 = new Font(); // Bold font
        Bold bold = new Bold();
        font1.Append(bold);
        Font font2 = new Font();
        Fonts fonts = new Fonts(); // <APENDING Fonts>

        fonts.Append(font0);
        fonts.Append(font1);
        fonts.Append(font2);


        // <Fills>
        Fill fill0 = new Fill(); // Default fill

        Fills fills = new Fills(); // <APENDING Fills>

        fills.Append(fill0);
        // <Borders>
        Border border0 = new Border(); // Default border

        Borders borders = new Borders(); // <APENDING Borders>
        borders.Append(border0);

        //alignments
        Alignment alg1 = new Alignment();
        alg1.Horizontal = HorizontalAlignmentValues.Center;
        Alignment alg2 = new Alignment();
        alg2.Horizontal = HorizontalAlignmentValues.Center;

        // <CellFormats>
        CellFormat cellformat0 = new CellFormat();
        cellformat0.FontId = 0;
        cellformat0.FillId = 0;
        cellformat0.BorderId = 0; // Default style : Mandatory | Style ID =0

        CellFormat cellformat1 = new CellFormat();
        cellformat1.FontId = 1; // Style with Bold text ; Style ID = 1
        cellformat1.Alignment = alg1;

        CellFormat cellformat2 = new CellFormat();
        cellformat2.FontId = 2; // Style with Bold text ; Style ID = 1
        cellformat2.Alignment = alg2;

        // <APENDING CellFormats>
        CellFormats cellformats = new CellFormats();
        cellformats.Append(cellformat0);
        cellformats.Append(cellformat1);
        cellformats.Append(cellformat2);

        // Append FONTS, FILLS , BORDERS & CellFormats to stylesheet <Preserve the ORDER>
        workbookstylesheet.Append(fonts);
        workbookstylesheet.Append(fills);
        workbookstylesheet.Append(borders);
        workbookstylesheet.Append(cellformats);

        // Finalize
        stylesheet.Stylesheet = workbookstylesheet;
        stylesheet.Stylesheet.Save();

        return stylesheet;
    }


    private static DocumentFormat.OpenXml.Spreadsheet.CellValues GetColumnType(System.Type v)
    {
        if (v == System.Type.GetType("System.Int32") || v == System.Type.GetType("System.Int64") || v == System.Type.GetType("System.Decimal") || v == System.Type.GetType("System.Double") || v == System.Type.GetType("System.Int16"))
        {
            return DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
        }
        return DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
    }

}
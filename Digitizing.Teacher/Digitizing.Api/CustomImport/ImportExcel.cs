using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Library.Common.Helper;


namespace Digitizing.Api.CustomImport
{
    public class ImportExcel : ExcelHelper
    {
        public static DataTable ReadFromExcelFileForSubjectScore(string path, int headerRow, out string messageError)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    messageError = "FILE_NOT_EXISTS";
                    return null;
                }

                if (!string.IsNullOrEmpty(path) && Path.HasExtension(path) && Path.GetExtension(path)!.ToLower() != ".xlsx")
                {
                    messageError = "WRONG_FORMAT_FILE";
                    return null;
                }

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorkbook workbook = excelPackage.Workbook;
                    if (workbook == null || workbook.Worksheets.Count == 0)
                    {
                        messageError = "EMPTY_DATA";
                        return null;
                    }

                    ExcelWorksheet excelWorksheet = workbook.Worksheets.First();
                    int num = 0;
                    foreach (ExcelRangeBase item in excelWorksheet.Cells[headerRow, 1, headerRow, excelWorksheet.Dimension.End.Column])
                    {
                        if (item.Start.Column != 2 && item.Start.Column != 3 && item.Start.Column != 4)
                        {
                            string value = item.Text.Trim() ?? "";
                            if (string.IsNullOrEmpty(value))
                            {
                                break;
                            }
                        }
                        num++;
                    }

                    dataTable.Columns.Add("subject_id");
                    dataTable.Columns.Add("subject_name");
                    dataTable.Columns.Add("number_credits");
                    dataTable.Columns.Add("student_rcd");
                    dataTable.Columns.Add("score");

                    for(int y = 5; y <= num; y++)
                    {
                        ExcelRange excelRange = excelWorksheet.Cells[1, 1, 1, num];
                        string subject = excelRange[1, y].Text;
                        string[] subject_info = subject.Split('-');
                        string subject_id = subject_info[0].Trim();
                        string subject_name = subject_info[1].Trim();
                        int number_credits = Convert.ToInt32(excelRange[2, y].Text);

                        for(int i = headerRow + 3; i <= excelWorksheet.Dimension.End.Row; i++)
                        {
                            ExcelRange excelRange2 = excelWorksheet.Cells[i, 1, i, y];
                            DataRow dataRow = dataTable.NewRow();
                            dataRow[0] = subject_id;
                            dataRow[1] = subject_name;
                            dataRow[2] = number_credits;

                            bool flag = false;
                            string text = "";

                            int index = 3;

                            for (int j = 1; j <= y; j++)
                            {
                                if (j == 1 || j == y)
                                {
                                    if (excelRange2[i, j] != null)
                                    {
                                        flag = true;
                                    }
                                    dataRow[index] = excelRange2[i, j].Value;
                                    text += excelRange2[i, j].Value ?? "";
                                    index++;
                                }
                            }

                            if (flag && !string.IsNullOrEmpty(text.Trim()))
                            {
                                dataTable.Rows.Add(dataRow);
                            }

                            if (string.IsNullOrEmpty(text.Trim()))
                            {
                                break;
                            }
                        }

                    }
                }

                messageError = "";
            }
            catch (Exception ex)
            {
                messageError = "ERROR:" + ex.Message;
            }

            return dataTable;
        }

        public static DataTable ReadFromExcelFileForPointTraining(string path, int headerRow, out string messageError)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    messageError = "FILE_NOT_EXISTS";
                    return null;
                }

                if (!string.IsNullOrEmpty(path) && Path.HasExtension(path) && Path.GetExtension(path)!.ToLower() != ".xlsx")
                {
                    messageError = "WRONG_FORMAT_FILE";
                    return null;
                }

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorkbook workbook = excelPackage.Workbook;
                    if (workbook == null || workbook.Worksheets.Count == 0)
                    {
                        messageError = "EMPTY_DATA";
                        return null;
                    }

                    ExcelWorksheet excelWorksheet = workbook.Worksheets.First();
                    int num = 0;
                    foreach (ExcelRangeBase item in excelWorksheet.Cells[headerRow, 1, headerRow, excelWorksheet.Dimension.End.Column])
                    {
                        if(item.Start.Column == 1 ||item.Start.Column == 5)
                        {
                            string value = item.Text.Trim() ?? "";
                            if (string.IsNullOrEmpty(value))
                            {
                                break;
                            }

                            num++;
                            //dataTable.Columns.Add(item.Text.Trim());
                        }
                    }

                    dataTable.Columns.Add("student_rcd");
                    //dataTable.Columns.Add("student_name");
                    //dataTable.Columns.Add("class_id");
                    //dataTable.Columns.Add("date_of_birth");
                    dataTable.Columns.Add("point");

                    for (int i = headerRow + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                    {
                        ExcelRange excelRange = excelWorksheet.Cells[i, 1, i, num + 3];
                        DataRow dataRow = dataTable.NewRow();
                        bool flag = false;
                        string text = "";
                        int index = 0;
                        foreach (ExcelRangeBase item2 in excelRange)
                        {
                            if (item2.Start.Column == 1 || item2.Start.Column == 5)
                            {
                                if (item2 != null)
                                {
                                    flag = true;
                                }
                                dataRow[index] = item2.Value;
                                text += ((item2.Value != null) ? item2.Value : "");
                                index++;
                            }
                        }

                        if (flag && !string.IsNullOrEmpty(text.Trim()))
                        {
                            dataTable.Rows.Add(dataRow);
                        }

                        if (string.IsNullOrEmpty(text.Trim()))
                        {
                            break;
                        }
                    }
                }

                messageError = "";
            }
            catch (Exception ex)
            {
                messageError = "ERROR:" + ex.Message;
            }

            return dataTable;
        }
        public static DataTable ReadFromExcelFileForTuitionFee(string path, out string messageError)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    messageError = "FILE_NOT_EXISTS";
                    return null;
                }

                if (!string.IsNullOrEmpty(path) && Path.HasExtension(path) && Path.GetExtension(path)!.ToLower() != ".xlsx")
                {
                    messageError = "WRONG_FORMAT_FILE";
                    return null;
                }

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorkbook workbook = excelPackage.Workbook;
                    if (workbook == null || workbook.Worksheets.Count == 0)
                    {
                        messageError = "EMPTY_DATA";
                        return null;
                    }

                    dataTable.Columns.Add("student_rcd");
                    dataTable.Columns.Add("owe_tuition_fee_last_semester");
                    dataTable.Columns.Add("owe_TATC_last_semester");
                    dataTable.Columns.Add("tuition_fee_to_be_paid");
                    dataTable.Columns.Add("TATC_to_be_paid");
                    dataTable.Columns.Add("tuition_fee_exemption");
                    dataTable.Columns.Add("tuition_fee_paid");
                    dataTable.Columns.Add("refund_of_tuition_fee");
                    dataTable.Columns.Add("TATC_paid");
                    dataTable.Columns.Add("lack_or_excess_of_TATC");
                    dataTable.Columns.Add("lack_or_excess_of_tuition_fee");
                    dataTable.Columns.Add("note");

                    //Start with row number 10
                    int headerRow = 10;
                    //Start with column number 2
                    int startColumn = 2;

                    ExcelWorksheet excelWorksheet = workbook.Worksheets.First();
                    var mergedCells = excelWorksheet.MergedCells;

                    //Get the index of the first cell in each merged cell and save it to a list of integers
                    List<int> mergedCellIndexes = new List<int>()
                    {
                        7, 14, 17, 18, 20, 22, 24, 27, 29, 31, 33, 37
                    };

                    // Subtract 6 rows
                    for (int i = headerRow + 1; i <= excelWorksheet.Dimension.End.Row - 6; i++)
                    {
                        ExcelRange excelRange = excelWorksheet.Cells[i, startColumn, i, excelWorksheet.Dimension.End.Column];
                        DataRow dataRow = dataTable.NewRow();
                        bool flag = false;
                        string text = "";
                        int index = 0;
                        bool isSkipRow = false;

                        if (excelRange[i, 2].Value.ToString().Trim() != "Lớp:")
                        {
                            foreach (int index2 in mergedCellIndexes)
                            {
                                if (excelRange[i, index2] != null)
                                {
                                    flag = true;
                                }
                                dataRow[index] = excelRange[i, index2].Value;
                                text += ((excelRange[i, index2].Value != null) ? excelRange[i, index2].Value : "");
                                index++;
                            }
                        }
                        else
                        {
                            isSkipRow = true;
                        }

                        //foreach (ExcelRangeBase item in excelRange)
                        //{
                        //    // Skip row
                        //    if (item.Start.Column == 2)
                        //    {
                        //        ExcelRange mergedRange = excelWorksheet.Cells[item.Start.Row, item.Start.Column, item.End.Row, item.End.Column];

                        //        if (mergedRange.Value.ToString().Trim() == "Lớp:")
                        //        {
                        //            isSkipRow = true;
                        //            break;
                        //        }
                        //        isSkipRow = false;
                        //    }
                        //    // Skip column "STT" and "Họ và tên"
                        //    if (mergedCellIndexes.Contains(item.Start.Column))
                        //    {
                        //        if (item != null)
                        //        {
                        //            flag = true;
                        //        }
                        //        dataRow[index] = item.Value;
                        //        text += ((item.Value != null) ? item.Value : "");
                        //        index++;
                        //    }
                        //}

                        if (flag && !string.IsNullOrEmpty(text.Trim()))
                        {
                            dataTable.Rows.Add(dataRow);
                        }

                        if (!isSkipRow && string.IsNullOrEmpty(text.Trim()))
                        {
                            break;
                        }
                    }
                }

                messageError = "";
            }
            catch (Exception ex)
            {
                messageError = "ERROR:" + ex.Message;
            }

            return dataTable;
        }
    }
}

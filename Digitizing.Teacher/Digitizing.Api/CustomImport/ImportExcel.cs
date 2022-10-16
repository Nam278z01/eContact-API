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
                    dataTable.Columns.Add("student_name");
                    dataTable.Columns.Add("class_id");
                    dataTable.Columns.Add("date_of_birth");
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
    }
}

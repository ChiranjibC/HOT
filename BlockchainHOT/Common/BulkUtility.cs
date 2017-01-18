using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace BlockchainHOT.Common
{
    public static class BulkUtility
    {
        public static DataSet GetDataSet(HttpPostedFileBase fileDoc)
        {
            DataSet result = null;
            if (System.IO.Path.GetExtension(fileDoc.FileName) == ".csv" || System.IO.Path.GetExtension(fileDoc.FileName) == ".txt")
                result = ConvertCSVtoDataTable(fileDoc);
            else
                result = GetDataSetFromExcel(fileDoc);

            return result;
        }
        private static DataSet GetDataSetFromExcel(HttpPostedFileBase fileDoc)
        {
            DataSet result = null;
            string modelName = string.Empty;
            string workSheetName = string.Empty;
            IExcelDataReader excelReader = null;
            if (System.IO.Path.GetExtension(fileDoc.FileName) == ".xls")
                excelReader = ExcelReaderFactory.CreateBinaryReader(fileDoc.InputStream);
            else if (System.IO.Path.GetExtension(fileDoc.FileName) == ".xlsx" || System.IO.Path.GetExtension(fileDoc.FileName) == ".xlsb" || System.IO.Path.GetExtension(fileDoc.FileName) == ".xlsm")
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileDoc.InputStream);            
            else
                excelReader = null;

            if (excelReader != null)
            {
                excelReader.IsFirstRowAsColumnNames = true;

                result = excelReader.AsDataSet();
                excelReader.Close();
            }


            return result;
        }

        private static DataSet ConvertCSVtoDataTable(HttpPostedFileBase fileDoc)
        {
            DataSet ds = new DataSet();
            DataTable dt = GetEmptyDataTable();
            using (StreamReader sr = new StreamReader(fileDoc.InputStream))
            {
                string[] headers = sr.ReadLine().Split(',');
                
                while (!sr.EndOfStream)
                {
                    string[] rowDataItems = sr.ReadLine().Split(',');
                    if (rowDataItems.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rowDataItems[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            ds.Tables.Add(dt);
            ds.AcceptChanges();
            return ds;
        }

        private static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Temperature");
            dt.Columns.Add("LogTime");
            return dt;
        }

    }
}
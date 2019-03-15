/**  
* 标    题：   ExcelMgr.cs 
* 描    述：    
* 创建时间：   2017年12月18日 11:02 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;

namespace Divak.Script.Editor 
{
	public class ExcelMgr
    {
        public static IWorkbook ReadExcel(string path)
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            if (path.IndexOf(".xlsx") > 0) // 2007版本  
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
            }
            else if (path.IndexOf(".xls") > 0) // 2003版本  
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
            }
            fileStream.Close();
            return workbook;
        }

        public static List<string> GetSheetsName(IWorkbook workbook)
        {
            List<string> list = new List<string>();
            int count = workbook.NumberOfSheets;
            for(int i = 0; i < count; i ++)
            {
                string name = GetSheetName(workbook, i);
                if (string.IsNullOrEmpty(name))
                {
                    if (EditorUtility.DisplayDialog("错  误", string.Format("GetSheetsName 分页{0}第{1}分页名为空", name, i), "确定"))
                        return null;
                    return null;
                }
                list.Add(name);
            }
            return list;
        }

        public static string GetSheetName(IWorkbook workbook, int index)
        {
            if (workbook == null) return null;
            return workbook.GetSheetName(index);
        }

        /// <summary>
        /// 获取配置表分页字段标题
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSheetTitle(IWorkbook workbook, string name)
        {
            List<string> list = new List<string>();
            ISheet sheet = workbook.GetSheet(name);

            IRow row = GetSheetRow(sheet, 0);
            if (row == null) return null;
            for(int i = 0; i < row.LastCellNum; i ++)
            {
                string value = row.GetCell(i).ToString();
                if(string.IsNullOrEmpty(value))
                {
                    if (EditorUtility.DisplayDialog("错  误", string.Format("GetSheetTitle 分页{0}第{1}字段为空", name, i), "确定"))
                        return null;
                    return null;
                }
                list.Add(value);
            }
            return list;
        }

        public static IRow GetSheetRow(ISheet sheet, int index)
        {
            if(sheet == null)
            {
                if (EditorUtility.DisplayDialog("错  误", "GetSheetRow sheet is null ", "确定"))
                    return null;
                return null;
            }
            return sheet.GetRow(index);
        }

        public static void CloseExcel(IWorkbook workbook)
        {
            if (workbook == null) return;
            workbook.Close();
            workbook = null;
        }
	}
}
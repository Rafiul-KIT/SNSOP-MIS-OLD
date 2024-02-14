using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


public static class Extensions
{
    public static StringBuilder ToCSV(this DataTable dtDataTable)
    {
        StringBuilder sb = new StringBuilder();
        // StreamWriter sw = new StreamWriter(strFilePath, false);
        //headers  
        for (int i = 0; i < dtDataTable.Columns.Count; i++)
        {
            sb.Append(dtDataTable.Columns[i]);
            if (i < dtDataTable.Columns.Count - 1)
            {
                sb.Append(",");
            }
        }
        sb.AppendLine();

        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(','))
                    {
                        value = String.Format("\"{0}\"", value);
                        sb.Append(value);
                    }
                    else
                    {
                        sb.Append(dr[i].ToString());
                    }
                }
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.AppendLine();
        }

        return sb;

    }
}

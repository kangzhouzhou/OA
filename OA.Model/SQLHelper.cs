using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OA.Model
{
    /// <summary>
    /// SQL Server帮助器
    /// </summary>
    public static class SQLHelper
    {
        //获取连接字符串
        public static string ConStr
        {
            get
            {
                if (string.IsNullOrEmpty(_conStr))
                {
                    _conStr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                }
                return _conStr;
            }
        }

        //连接字符串
        private static string _conStr;

        //获取报表主键
        public static int GetReportKeyValue()
        {
            string comStr = "GetReportKey";
            using (SqlConnection connection = new SqlConnection(SQLHelper.ConStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //设置执行的为存储过程
                    command.CommandType = CommandType.StoredProcedure;
                    //执行的的存储名称
                    command.CommandText = comStr;
                    //
                    SqlParameter keyValue = new SqlParameter() { ParameterName = "@keyValue", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                    command.Parameters.Add(keyValue);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)keyValue.Value;
                }
            }
        }

        //获取指定表结构
        public static DataTable GetTableStruct(string tableName)
        {
            string commandStr = "SELECT * FROM " + tableName + " WHERE 1=2";
            DataTable dTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(commandStr, SQLHelper.ConStr))
            {
                adapter.Fill(dTable);
                dTable.TableName = tableName;
            }
            return dTable;
        }

        // 根据指定的表名，返回该表的默认视图
        public static DataView SQLDefaultDataView(string commandStr, bool strType = true)
        {
            if (strType)
                commandStr = "SELECT * FROM " + commandStr + ";";
            DataSet dSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(commandStr, SQLHelper.ConStr))
            {
                adapter.Fill(dSet);
            }
            return dSet.Tables[0].DefaultView;
        }

        // 根据指定的表名，返回该表的默认视图
        public static DataView SQLDefaultDataView(string commandStr, bool strType = true, params SqlParameter[] paramsArray)
        {
            if (strType)
                commandStr = "SELECT * FROM " + commandStr + ";";
            DataSet dSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(commandStr, SQLHelper.ConStr))
            {
                if (paramsArray != null && paramsArray.Length > 0)
                {
                    adapter.SelectCommand.Parameters.AddRange(paramsArray);
                }
                adapter.Fill(dSet);
            }
            return dSet.Tables[0].DefaultView;
        }

        // 根据指定的SQL语句，返回指定的数据集
        public static DataSet SQLDataSet(string comStr, params SqlParameter[] paramsArray)
        {
            DataSet dSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(comStr, SQLHelper.ConStr))
            {
                if (paramsArray != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(paramsArray);
                }
                adapter.Fill(dSet);
            }
            return dSet;
        }

        //更新DataTable,comStr获取原来数据源的语句,updateDataTable =_argDataSet.Tables[1].GetChanges();
        public static bool SQLUpdateDataTable(DataTable sourceData, string comStr)
        {
            DataSet dSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(comStr, SQLHelper.ConStr))
            {
                using (SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(adapter))
                {
                    int changeCount = adapter.Update(sourceData);
                    if (changeCount == 0)
                        return false;
                    return true;
                }
            }
        }

        //单行单列并进行类型转换
        public static T SQLExecuteScalar<T>(string comStr, params SqlParameter[] paramsArray)
        {
            using (SqlConnection connection = new SqlConnection(SQLHelper.ConStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = comStr;
                        if (paramsArray != null && paramsArray.Length > 0)
                        {
                            command.Parameters.AddRange(paramsArray);
                        }
                        connection.Open();
                        object obj = command.ExecuteScalar();
                        T returnValue = (T)obj;
                        return returnValue;
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        connection.Dispose();
                        throw ex;
                    }
                }
            }
        }

        //单行单列，不进行类型转换
        public static Object SQLExecuteScalar(string comStr, params SqlParameter[] paramsArray)
        {
            using (SqlConnection connection = new SqlConnection(SQLHelper.ConStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = comStr;
                        if (paramsArray != null && paramsArray.Length > 0)
                        {
                            command.Parameters.AddRange(paramsArray);
                        }
                        connection.Open();
                        object returnValue = command.ExecuteScalar();
                        return returnValue;
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        connection.Dispose();
                        throw ex;
                    }
                }
            }
        }

        //ExecuteNonQuery封装
        public static int SQLExecuteNonQuery(string comStr, params SqlParameter[] paramsArray)
        {
            using (SqlConnection connection = new SqlConnection(SQLHelper.ConStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = comStr;
                        if (paramsArray != null && paramsArray.Length > 0)
                        {
                            command.Parameters.AddRange(paramsArray);
                        }
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }
        }

        //多个表进行数据插入操作
        public static bool SQLInsertDataSet(DataSet dSet)
        {
            StringBuilder sb = new StringBuilder();
            //拼接执行语句
            for (int i = 0; i < dSet.Tables.Count; i++)
            {
                for (int k = 0; k < dSet.Tables[i].Rows.Count; k++)
                {
                    //INSERT及列名
                    sb.Append("INSERT INTO " + dSet.Tables[i].TableName + " ");
                    sb.Append("(");
                    for (int j = 0; j < dSet.Tables[i].Columns.Count; j++)
                    {
                        //列名
                        sb.Append(dSet.Tables[i].Columns[j].ColumnName + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")");
                    sb.Append(" VALUES(");
                    //插入VALUES值
                    for (int j = 0; j < dSet.Tables[i].Columns.Count; j++)
                    {
                        switch (dSet.Tables[i].Columns[j].DataType.FullName.ToString())
                        {
                            //    case "System.Int32":
                            //        //处理NULL值
                            //        if (dSet.Tables[i].Rows[k][j].ToString() == "")
                            //            sb.Append("NULL");
                            //        else
                            //            sb.Append(dSet.Tables[i].Rows[k][j].ToString() + ",");
                            //        break;
                            default:
                                //处理NULL值
                                if (dSet.Tables[i].Rows[k][j].ToString() == "")
                                    sb.Append("NULL,");
                                else
                                    sb.Append("'" + dSet.Tables[i].Rows[k][j].ToString() + "',");
                                break;
                        }
                    }
                    //进行处理最后一个列值是否为数字问题
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")");
                    sb.Append(";");
                }
            }
            //开启事务，进行插入
            using (SqlConnection connection = new SqlConnection(SQLHelper.ConStr))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //先打开连接
                    connection.Open();
                    SqlTransaction tranc = connection.BeginTransaction();
                    command.Transaction = tranc;
                    command.CommandText = sb.ToString();

                    try
                    {
                        command.ExecuteNonQuery();
                        tranc.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tranc.Rollback();
                        //捕获异常进行日志写入
                        string filePath = System.Environment.CurrentDirectory + "CustomeReportErrLog.txt";
                        File.WriteAllText(filePath, "==================================================================================================="
                           + System.Environment.NewLine
                           + "时间：   " + DateTime.Now + System.Environment.NewLine
                           + "信息：   " + ex.Message + System.Environment.NewLine
                           + "事件源:  " + ex.Source + System.Environment.NewLine
                           + "堆栈信息:" + ex.StackTrace + System.Environment.NewLine);
                        return false;
                    }
                }
            }
        }

        public static DataSet SQLExecuteProcedure(string procedureStr, params SqlParameter[] paramArray)
        {
            DataSet resultDataSet=new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(procedureStr, SQLHelper.ConStr))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (paramArray != null)
                    adapter.SelectCommand.Parameters.AddRange(paramArray);
                adapter.Fill(resultDataSet);
            }
            return resultDataSet;
        }
    }
}

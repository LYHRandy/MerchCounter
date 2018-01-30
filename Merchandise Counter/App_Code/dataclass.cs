using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

public class dataclass
{
    string connection = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();
    MySqlDataAdapter adapt;
    DataSet ds;
    public dataclass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string RequesttStr(string inputStr)
    {
        inputStr = inputStr.ToLower().Replace(",", "");
        inputStr = inputStr.ToLower().Replace("<", "");
        inputStr = inputStr.ToLower().Replace(">", "");
        inputStr = inputStr.ToLower().Replace("%", "");
        inputStr = inputStr.ToLower().Replace(".", "");
        inputStr = inputStr.ToLower().Replace(":", "");
        inputStr = inputStr.ToLower().Replace("#", "");
        inputStr = inputStr.ToLower().Replace("&", "");
        inputStr = inputStr.ToLower().Replace("$", "");
        inputStr = inputStr.ToLower().Replace("^", "");
        inputStr = inputStr.ToLower().Replace("*", "");
        inputStr = inputStr.ToLower().Replace("`", "");
        inputStr = inputStr.ToLower().Replace(" ", "");
        inputStr = inputStr.ToLower().Replace("~", "");
        inputStr = inputStr.ToLower().Replace("or", "");
        inputStr = inputStr.ToLower().Replace("and", "");
        inputStr = inputStr.ToLower().Replace("'", "");
        //以上的意思为前者被后者代替，比如and代替为空，<代替为：&glt;
        return inputStr;

    }

    /// <summary>
    /// 创建access数据库连接
    /// </summary>
    /// <returns></returns>

    //public static OleDbConnection creation()
    //{
    //    return new OleDbConnection(ConfigurationManager.ConnectionStrings["piscineSplashConString"].ConnectionString.ToString());

    //}

    public void ExecuteSQL(string strSQL)
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strSQL, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message, err);
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
    }

    public string ExecScalar(string strSql)
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            try
            {
                conn.Open();
                MySqlCommand cmdstr = new MySqlCommand(strSql);
                strSql = Convert.ToString(cmdstr.ExecuteScalar());
                return strSql;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message, err);
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
    }

    public String CreateRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }

    public bool IsValidEmail(string email)
    {
        //regular expression pattern for valid email
        string pattern = @".*@.*\..*";
        //Regular expression object
        System.Text.RegularExpressions.Regex check = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
        bool valid = false;

        if (string.IsNullOrEmpty(email))
        {
            valid = true;
        }
        else
        {
            valid = check.IsMatch(email);
        }
        return valid;
    }

    public DataTable GetDataSet(string strSql, string TableName)
    {
        ds = new DataSet();
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            try
            {
                conn.Open();//与数据库连接    
                adapt = new MySqlDataAdapter(strSql, conn); //实例化SqlDataAdapter类对象S
                adapt.Fill(ds, TableName);//填充数据集    
                return ds.Tables[TableName];//返回数据集DataSet的表的集合    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
            finally
            {//断开连接，释放资源    
                conn.Dispose();
                adapt.Dispose();
                conn.Close();
            }
        }
    }
    public string GetSingleValue(String strSql)
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            string result;
            try
            {
                conn.Open();
                MySqlCommand cmdstr = new MySqlCommand(strSql, conn);

                result = cmdstr.ExecuteScalar().ToString();

                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message, err);
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
        }
    }
    public static string FormatString(string str)
    {
        str = str.Replace(" ", "&nbsp;&nbsp");//控制格式含数
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&glt;");
        str = str.Replace('\n'.ToString(), "<br>");
        return str;
    }
    public static string ReplaceStr(string str)
    {
        if (str == null || str == "")
            return null;
        str = str.Replace(":", "");
        str = str.Replace("'", "");
        str = str.Replace("&", "");
        str = str.Replace("%20", "");
        str = str.Replace("-", "");
        str = str.Replace("==", "");
        str = str.Replace("<", "");
        str = str.Replace(">", "");
        str = str.Replace("%", " ");
        return str;
    }

    public static string DelSQLStr(string str)
    {
        if (str == null || str == "")

            return "";
        str = str.Replace(";", "");
        str = str.Replace("'", "");
        str = str.Replace("&", "");
        str = str.Replace("%20", "");
        str = str.Replace("--", "");
        str = str.Replace("==", "");
        str = str.Replace("<", "");
        str = str.Replace(">", "");
        str = str.Replace("%", "");
        str = str.Replace("+", "");
        str = str.Replace("-", "");
        str = str.Replace("=", "");
        str = str.Replace(",", " ");
        return str;
    }
}


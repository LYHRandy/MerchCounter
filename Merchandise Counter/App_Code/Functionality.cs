using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

	public class Functionality
    {
        public Functionality()
	    {
		//
		// TODO: Add constructor logic here
		//
	    }
        
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();
        private int mint_CommandTimeout = 30;
        private MySqlConnection mobj_SqlConnection;
        private MySqlCommand mobj_SqlCommand;
        private string mstr_ConnectionString;
        public string replaceForSQL(string str)
        {
            return str.Replace("'", "");
           
        }
        public string solveSQL(string str)
        {
            return str.Replace("'", "''");
        }
        public void ExecuteSQL(string sCommand)
        {
            try
            {
                mstr_ConnectionString = ConnectionString;

                mobj_SqlConnection = new MySqlConnection(mstr_ConnectionString);
                mobj_SqlCommand = new MySqlCommand();
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.Connection = mobj_SqlConnection;

                mobj_SqlCommand.CommandText = sCommand;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;

                mobj_SqlConnection.Open();

                mobj_SqlCommand.ExecuteNonQuery();

                //DataSet ds = new DataSet();
                //adpt.Fill(ds, sDatatable);
                //return ds;
            }
            catch ( Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public String CreateRandomCode(int passwordLength)
        {
            string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        public DataSet GetDatasetByCommand(string sCommand, string sDatatable)
        {
            try
            {
                mstr_ConnectionString = ConnectionString;

                mobj_SqlConnection = new MySqlConnection(mstr_ConnectionString);
                mobj_SqlCommand = new MySqlCommand();
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.Connection = mobj_SqlConnection;

                mobj_SqlCommand.CommandText = sCommand;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;

                mobj_SqlConnection.Open();

                MySqlDataAdapter adpt = new MySqlDataAdapter(mobj_SqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds, sDatatable);
                return ds;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        public String encrypt(String str, int key)
        {
            String result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if ((int)str[i] + key > 126)
                    result = result + Convert.ToChar((((int)str[i] + key) - 127) + 32);//((int (word[x]) + key) - 127) + 32
                else
                    result = result + Convert.ToChar(((int)str[i] + key));
            }
            return result;
        }

        public String CreateRandomNumber(int passwordLength)
        {
            string allowedChars = "0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        public void CloseConnection()
        {
            if (mobj_SqlConnection.State != ConnectionState.Closed) mobj_SqlConnection.Close();
        }
        public String GetDataByCommand(string Command, string FieldName)
        {
            try
            {
                mstr_ConnectionString = ConnectionString;

                mobj_SqlConnection = new MySqlConnection(mstr_ConnectionString);
                mobj_SqlCommand = new MySqlCommand();
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
                mobj_SqlCommand.Connection = mobj_SqlConnection;

                mobj_SqlCommand.CommandText = Command;
                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;

                mobj_SqlConnection.Open();

                MySqlDataReader reader = mobj_SqlCommand.ExecuteReader();
                string svalue = "";
                if (reader.Read())
                {
                    svalue = reader[FieldName].ToString();
                }
                else
                {
                    svalue = "0";
                }
                return svalue;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
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
        public bool IsValidNumeric(string sValue)
        {
            //regular expression pattern for valid email
            string pattern = @"^\d+$";
            //Regular expression object
            System.Text.RegularExpressions.Regex check = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
            bool valid = false;

            if (string.IsNullOrEmpty(sValue))
            {
                valid = true;
            }
            else
            {
                valid = check.IsMatch(sValue);
            }
            return valid;
        }
        public string getDecryptURL(string sParam)
        {
            string sEncoded = sParam.Replace("'", "");
            sEncoded = sEncoded.Replace("||", "+");
            sParam = Core_App.RijndaelSimple.Decrypt(sEncoded, "Pas5pr@se", "s@1tValue", "MD5", 2, "@1B2c3D4e5F6g7H8", 256);
            return sParam;
        }
        public string setEncryptURL(string sParam)
        {
            string sEncoded = sParam.Replace("'", "");
            sParam = Core_App.RijndaelSimple.Encrypt(sEncoded, "Pas5pr@se", "s@1tValue", "MD5", 2, "@1B2c3D4e5F6g7H8", 256);
            return sParam.Replace("+", "||");
        }
        public bool IsFaculty(string UserCategory)
        {
            bool IsFac = false;
            if (UserCategory == GetConfigAppValue("FacultyValue"))
                IsFac = true;
            return IsFac;
        }

        public string GetConfigAppValue(string Key)
        {
            string value = "";
            try
            {
                value = ConfigurationManager.AppSettings[Key];
            }
            catch (Exception ex) { }
            return value;
        }
	}
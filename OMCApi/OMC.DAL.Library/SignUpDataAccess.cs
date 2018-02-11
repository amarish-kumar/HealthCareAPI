using System;
using OMC.Models;
using OMC.DAL.Interface;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace OMC.DAL.Library
{
    public class SignUpDataAccess : DataAccessBase, ISignUpDataAccess
    {
        #region Declaration
        SqlConnection _connection;
        SqlCommand _command;
        #endregion

        #region Constructor
        public SignUpDataAccess()
        {
            this._connection = Connection;
            this._command = Command;
        }
        #endregion

        #region Methods

        public bool InitiateSignUpProcess(UserSignUp signupdetails)
        {
            try
            {
                Log.Info("Started call to InitiateSignUpProcess");
                Log.Info("parameter values" + JsonConvert.SerializeObject(signupdetails));
                Command.CommandText = "SP_USER_DETAIL_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                SqlDataAdapter da = new SqlDataAdapter(Command);
                da.SelectCommand.Parameters.Add(new SqlParameter("@USER_DETAIL_XML", SqlDbType.NVarChar, 10000));
                da.SelectCommand.Parameters["@USER_DETAIL_XML"].Value = GetXMLFromObject(signupdetails);
                da.SelectCommand.Parameters.Add(new SqlParameter("@OPERATION", SqlDbType.NVarChar, 100));
                da.SelectCommand.Parameters["@OPERATION"].Value = !string.IsNullOrEmpty(signupdetails.UserAction) ? signupdetails.UserAction : (object)DBNull.Value;
                da.SelectCommand.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.BigInt, 100));
                da.SelectCommand.Parameters["@USER_ID"].Value = !string.IsNullOrEmpty(signupdetails.LoggedInUserID) ? Convert.ToInt32(signupdetails.LoggedInUserID) : 1;
                Connection.Open();

                int? result = (int?)Command.ExecuteScalar();
                Log.Info("End call to InitiateSignUpProcess with result  = "+ result);

                if (result == null || result == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

        }

        #endregion
    }
}

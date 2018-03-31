using System;
using OMC.Models;
using OMC.DAL.Interface;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public bool InsertUpdateProfile(Profile profile)
        {
            try
            {
                Log.Info("Started call to InsertUpdateProfile");
                Log.Info("parameter values" + JsonConvert.SerializeObject(profile));
                Command.CommandText = "SP_PROFILE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@PROFILE_XML", GetXMLFromObject(profile));
                Command.Parameters.AddWithValue("@USER_ID", profile.UserId > 0 ? profile.UserId : 1);
                Connection.Open();
                Command.ExecuteNonQuery();
                Log.Info("End call to InsertUpdateProfile");
                return true;
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

        public List<Profile> GetProfileList(int userId, int? profileId)
        {
            try
            {
                Log.Info("Started call to GetProfileList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { userId = userId, profileId = profileId }));
                Command.CommandText = "SP_GET_PROFILES";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@USER_ID", userId);
                if (profileId.HasValue)
                {
                    Command.Parameters.AddWithValue("@PROFILE_ID", profileId);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                List<Profile> result = new List<Profile>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Profile
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            UserFirstName = reader["UserFirstName"] != DBNull.Value ? reader["UserFirstName"].ToString() : null,
                            UserLastName = reader["UserLastName"] != DBNull.Value ? reader["UserLastName"].ToString() : null,
                            UserId = Convert.ToInt32(reader["UserId"].ToString()),
                            FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                            LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                            DOB = reader["DOB"] != DBNull.Value ? reader["DOB"].ToString() : null,
                            GenderId = Convert.ToInt32(reader["GenderId"].ToString()),
                            GenderName = reader["GenderName"] != DBNull.Value ? reader["GenderName"].ToString() : null,
                            RelationshipId = Convert.ToInt32(reader["RelationshipId"].ToString()),
                            Relationship = reader["Relationship"] != DBNull.Value ? reader["Relationship"].ToString() : null,
                        });
                    }
                }
                Log.Info("End call to GetProfileList result " + JsonConvert.SerializeObject(result));

                return result;
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

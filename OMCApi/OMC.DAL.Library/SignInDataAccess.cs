using Newtonsoft.Json;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OMC.DAL.Library
{
    public class SignInDataAccess : DataAccessBase, ISignInDataAccess
    {
        #region Declaration
        #endregion

        #region Methods

        public SignInResponse InitiateSignInProcess(UserLogin user)
        {
            try
            {
                Log.Info("Started call to InitiateSignInProcess");
                Log.Info("parameter values" + JsonConvert.SerializeObject(user));
                Command.CommandText = "SP_LOGIN_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@USERNAME", user.Username);
                Command.Parameters.AddWithValue("@PASSWORD", user.Password);
                Command.Parameters.AddWithValue("@IP_ADDRESS", user.IPAddress);
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                SignInResponse result=new SignInResponse();
                if (ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        var resultRow = ds.Tables[0].Rows[0];
                        result = new SignInResponse
                        {
                            IsPasswordVerified = Convert.ToBoolean(resultRow["IS_PASSWORD_VERIFIED"]),
                            SessionId = resultRow["SESSION_ID"] != DBNull.Value ? resultRow["SESSION_ID"].ToString() : null,
                            TwoFactorAuthDone = Convert.ToBoolean(resultRow["TWO_FACTOR_AUTH_DONE"]),
                            IsUserActive = Convert.ToBoolean(resultRow["IS_USER_ACTIVE"]),
                            TwoFactorAuthTimestamp = !string.IsNullOrEmpty(resultRow["TWO_FACTOR_AUTH_TS"].ToString())
                                                   ? Convert.ToDateTime(resultRow["TWO_FACTOR_AUTH_TS"].ToString()) 
                                                   : (DateTime?)null,
                            UserDeviceId = !string.IsNullOrEmpty(resultRow["USER_DEVICE_ID"].ToString()) ? Convert.ToInt32(resultRow["USER_DEVICE_ID"].ToString()) : (int?)null,
                            UserId = !string.IsNullOrEmpty(resultRow["USER_ID"].ToString()) ? Convert.ToInt32(resultRow["USER_ID"].ToString()) : (int?)null
                        };

                        if (ds.Tables.Count > 1
                            && ds.Tables[1].Rows.Count > 0)
                        {
                            result.UserRoles = new List<UserRole>();
                            foreach (DataRow drRole in ds.Tables[1].Rows)
                            {
                                result.UserRoles.Add(new UserRole
                                {
                                    UserId = result.UserId.Value,
                                    RoleId = Convert.ToInt32(drRole["RoleId"].ToString()),
                                    RoleName = drRole["RoleName"].ToString(),
                                    IsDefault = Convert.ToBoolean(drRole["IsDefault"])
                                });
                            }
                        }
                    }
                }
                
                Log.Info("End call to InitiateSignInProcess");
                return result;

            }
            catch(Exception ex)
            {
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }

        public UserAccessCodeResponse GetAccessCode(UserLogin user)
        {
            try
            {
                Log.Info("Started call to GetAccessCode");
                Log.Info("parameter values" + JsonConvert.SerializeObject(user));
                Command.CommandText = "SP_GET_ACCESS_CODE";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@USER_ID", user.UserId);
                Command.Parameters.AddWithValue("@IP_ADDRESS", user.IPAddress);
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                UserAccessCodeResponse result = new UserAccessCodeResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new UserAccessCodeResponse
                        {
                            UserId = !string.IsNullOrEmpty(reader["UserId"].ToString()) ? Convert.ToInt32(reader["UserId"].ToString()) : (int?)null,
                            UserLoginAuditId = !string.IsNullOrEmpty(reader["UserLoginAuditId"].ToString()) ? Convert.ToInt32(reader["UserLoginAuditId"].ToString()) : (int?)null,
                            AccessCode = reader["AccessCode"] != DBNull.Value ? reader["AccessCode"].ToString() : null,
                            EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : null
                        };
                    }
                }

                Log.Info("End call to GetAccessCode");
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

        public void ValidateAccessCode(UserAccessCodeResponse userAccessCode)
        {
            try
            {
                Log.Info("Started call to ValidateAccessCode");
                Log.Info("parameter values: " + JsonConvert.SerializeObject(userAccessCode));
                Command.CommandText = "SP_VALIDATE_ACCESS_CODE";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@USER_ID", userAccessCode.UserId.Value);
                Command.Parameters.AddWithValue("@USER_LOGIN_AUDIT_ID", userAccessCode.UserLoginAuditId.Value);
                Command.Parameters.AddWithValue("@ACCESS_CODE", userAccessCode.AccessCode);
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                ValidateAccessCodeResponse result = new ValidateAccessCodeResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ValidateAccessCodeResponse
                        {
                            UnsuccessfulAttemptCount = Convert.ToInt32(reader["UnsuccessfulAttemptCount"].ToString()),
                            IsValidCodePassed = Convert.ToBoolean(reader["IsValidCodePassed"]),
                            Message = reader["Message"] != DBNull.Value ? reader["Message"].ToString() : null,
                            IsAccountLocked = Convert.ToBoolean(reader["IsAccountLocked"])
                        };
                    }
                }

                Log.Info("End call to ValidateAccessCode: " + JsonConvert.SerializeObject(result));
                userAccessCode.objValidateAccessCodeResponse = result;
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

        public Email GetEmailData(string emailType)
        {
            try
            {
                Log.Info("Started call to GetMessageBody");
                Log.Info("parameter values" + JsonConvert.SerializeObject(emailType));
                Command.CommandText = "SP_GET_EMAIL_TEXT";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@EMAIL_TYPE", emailType);
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                Email result = new Email();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new Email
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Body = reader["Body"] != DBNull.Value ? reader["Body"].ToString() : null,
                            Subject = reader["Subject"] != DBNull.Value ? reader["Subject"].ToString() : null,
                            EmailType = reader["EmailType"] != DBNull.Value ? reader["EmailType"].ToString() : null,
                            SenderAccountId = reader["SenderAccountId"] != DBNull.Value ? reader["SenderAccountId"].ToString() : null,
                            SenderAddress = reader["SenderAddress"] != DBNull.Value ? reader["SenderAddress"].ToString() : null,
                            SenderPassword = reader["SenderPassword"] != DBNull.Value ? reader["SenderPassword"].ToString() : null
                        };
                    }
                }

                Log.Info("End call to GetMessageBody");
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

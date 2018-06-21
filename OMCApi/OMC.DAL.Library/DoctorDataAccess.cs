using Newtonsoft.Json;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OMC.DAL.Library
{
    public class DoctorDataAccess : DataAccessBase, IDoctorDataAccess
    {
        #region Declaration
        #endregion

        #region Methods

        public DoctorProfileResponse InsertUpdateDoctorProfile(DoctorProfile doctorProfile, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorProfile");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorProfile = doctorProfile, operation = operation }));
                Command.CommandText = "SP_DOCTOR_PROFILE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_PROFILE_XML", GetXMLFromObject(doctorProfile));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (doctorProfile.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorProfile.AddedBy.Value);
                }
                if (doctorProfile.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorProfile.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorProfileResponse result = new DoctorProfileResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorProfileResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorProfile");

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

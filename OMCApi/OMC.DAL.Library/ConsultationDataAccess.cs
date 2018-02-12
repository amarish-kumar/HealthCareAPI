using Newtonsoft.Json;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OMC.DAL.Library
{
    public class ConsultationDataAccess : DataAccessBase, IConsultationDataAccess
    {
        #region Declaration
        #endregion

        #region Methods
        public List<ConsultationDisplay> GetConsultationList(int userId, string userRole = "Doctor")
        {
            try
            {
                Log.Info("Started call to GetConsultationList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { userId = userId, userRole = userRole }));
                Command.CommandText = "SP_GET_CONSULTATION_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@USER_ID", userId);
                Command.Parameters.AddWithValue("@USER_ROLE", userRole);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                List<ConsultationDisplay> result = new List<ConsultationDisplay>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add( new ConsultationDisplay
                        {
                            ConsultationDescription = reader["ConsultationDescription"] != DBNull.Value ? reader["ConsultationDescription"].ToString() : null,
                            ConsultationId = Convert.ToInt32(reader["ConsultationId"].ToString()),
                            DoctorName = reader["DoctorName"] != DBNull.Value ? reader["DoctorName"].ToString() : null,
                            DoctorId = Convert.ToInt32(reader["DoctorId"].ToString()),
                            PatientName = reader["PatientName"] != DBNull.Value ? reader["PatientName"].ToString() : null,
                            PatientId = Convert.ToInt32(reader["PatientId"].ToString()),
                            ConsultationStatus = reader["ConsultationStatus"] != DBNull.Value ? reader["ConsultationStatus"].ToString() : null,
                            ConsultationStatusId = Convert.ToInt32(reader["ConsultationStatusId"].ToString()),
                        });
                    }
                }
                Log.Info("End call to GetConsultationList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationResponse InitiateConsultation(Consultation consultationDetails)
        {
            try
            {
                Log.Info("Started call to InitiateConsultation");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationDetails));
                Command.CommandText = "SP_CONSULTATION_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_XML", GetXMLFromObject(consultationDetails));
                Command.Parameters.AddWithValue("@OPERATION", "CONSULTATION");
                //Command.Parameters.AddWithValue("@USER_ID", GetXMLFromObject(consultationDetails));
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationResponse result = new ConsultationResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InitiateConsultation");

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

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
                            ConsultationCreateDate = reader["ConsultationCreateDate"] != DBNull.Value ? DateTime.Parse(reader["ConsultationCreateDate"].ToString()) : (DateTime?)null,
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

        public ConversationResponse GetConversationList(int consultationId, int userId, string userRole = "Doctor")
        {
            try
            {
                Log.Info("Started call to GetConversationList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, userId = userId, userRole = userRole }));
                Command.CommandText = "SP_GET_CONVERSATION_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                Command.Parameters.AddWithValue("@USER_ID", userId);
                Command.Parameters.AddWithValue("@USER_ROLE", userRole);
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConversationResponse result = new ConversationResponse();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var resultRow = ds.Tables[0].Rows[0];
                        result.ConsultationObject = new ConsultationDisplay
                        {
                            ConsultationDescription = resultRow["ConsultationDescription"] != DBNull.Value ? resultRow["ConsultationDescription"].ToString() : null,
                            ConsultationId = Convert.ToInt32(resultRow["ConsultationId"].ToString()),
                            DoctorName = resultRow["DoctorName"] != DBNull.Value ? resultRow["DoctorName"].ToString() : null,
                            DoctorId = Convert.ToInt32(resultRow["DoctorId"].ToString()),
                            PatientName = resultRow["PatientName"] != DBNull.Value ? resultRow["PatientName"].ToString() : null,
                            PatientId = Convert.ToInt32(resultRow["PatientId"].ToString()),
                            ConsultationStatus = resultRow["ConsultationStatus"] != DBNull.Value ? resultRow["ConsultationStatus"].ToString() : null,
                            ConsultationStatusId = Convert.ToInt32(resultRow["ConsultationStatusId"].ToString()),
                            ConsultationCreateDate = resultRow["ConsultationCreateDate"] != DBNull.Value ? DateTime.Parse(resultRow["ConsultationCreateDate"].ToString()) : (DateTime?)null,
                        };

                        if (ds.Tables.Count > 1
                            && ds.Tables[1].Rows.Count > 0)
                        {
                            result.Conversations = new List<ConversationDisplay>();
                            foreach (DataRow drConversation in ds.Tables[1].Rows)
                            {
                                result.Conversations.Add(new ConversationDisplay
                                {
                                    ConversationId = Convert.ToInt32(drConversation["ConversationId"].ToString()),
                                    ConversationDescription = drConversation["ConversationDescription"] != DBNull.Value ? drConversation["ConversationDescription"].ToString() : null,
                                    ConsultationId = Convert.ToInt32(drConversation["ConsultationId"].ToString()),
                                    DoctorName = drConversation["DoctorName"] != DBNull.Value ? drConversation["DoctorName"].ToString() : null,
                                    DoctorId = drConversation["DoctorId"] != DBNull.Value ? int.Parse(drConversation["DoctorId"].ToString()) : (int?)null,
                                    PatientName = drConversation["PatientName"] != DBNull.Value ? drConversation["PatientName"].ToString() : null,
                                    PatientId = drConversation["PatientId"] != DBNull.Value ? int.Parse(drConversation["PatientId"].ToString()) : (int?)null,
                                    IsLocked = Convert.ToBoolean(drConversation["IsLocked"].ToString()),
                                    ConversationCreateDate = drConversation["ConversationCreateDate"] != DBNull.Value ? DateTime.Parse(drConversation["ConversationCreateDate"].ToString()) : (DateTime?)null
                                });
                            }
                        }
                    }
                }
                Log.Info("End call to GetConversationList result " + JsonConvert.SerializeObject(result));

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

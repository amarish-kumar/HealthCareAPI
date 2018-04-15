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
                            ProfileName = reader["ProfileName"] != DBNull.Value ? reader["ProfileName"].ToString() : null,
                            ProfileId = Convert.ToInt32(reader["ProfileId"].ToString()),
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
                            ProfileName = resultRow["ProfileName"] != DBNull.Value ? resultRow["ProfileName"].ToString() : null,
                            ProfileId = Convert.ToInt32(resultRow["ProfileId"].ToString()),
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
                if (consultationDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationDetails.AddedBy.Value);
                }
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
                
        public PatientEnquiryResponse UnregisteredPatientEnquiry(PatientEnquiry enquiry)
        {
            try
            {
                Log.Info("Started call to UnregisteredPatientEnquiry");
                Log.Info("parameter values" + JsonConvert.SerializeObject(enquiry));
                Command.CommandText = "SP_UNREGISTEREDPATIENT_ENQUIRY";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@Description", enquiry.Description);
                
                Connection.Open();

                PatientEnquiryResponse result = new PatientEnquiryResponse();

                result.Enquiries = new List<PatientEnquiry>();

                SqlDataAdapter dataAdaptereader = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdaptereader.Fill(ds);
 
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow resultRow in ds.Tables[0].Rows)
                        {
                            result.Enquiries.Add(new PatientEnquiry
                            {
                                SpecialityId = resultRow["SpecialityId"] != DBNull.Value ? resultRow["SpecialityId"].ToString() : null,
                                SpecialityDesc = resultRow["Speciality"] != DBNull.Value ? resultRow["Speciality"].ToString() : null,
                                DoctorId = resultRow["DoctorId"] != DBNull.Value ? resultRow["DoctorId"].ToString() : null,
                                DoctorName = resultRow["DoctorName"] != DBNull.Value ? resultRow["DoctorName"].ToString() : null
                            });
                        }
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
        
        public ConversationResponse RecordConversation(Conversation conversationDetails)
        {
            try
            {
                Log.Info("Started call to RecordConversation");
                Log.Info("parameter values" + JsonConvert.SerializeObject(conversationDetails));
                Command.CommandText = "SP_CONVERSATION_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONVERSATION_XML", GetXMLFromObject(conversationDetails));
                Command.Parameters.AddWithValue("@OPERATION", "CONVERSATION");
                if (conversationDetails.PatientId.HasValue || conversationDetails.DoctorId.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", conversationDetails.PatientId.HasValue 
                                                    ? conversationDetails.PatientId.Value 
                                                    : conversationDetails.DoctorId.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConversationResponse result = new ConversationResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConversationResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())                            
                        };
                    }
                }
                Log.Info("End call to RecordConversation");

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

        public ConsultationReportResponse InsertUpdateConsultationReport(ConsultationReports consultationReport)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationReport");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationReport));
                Command.CommandText = "SP_CONSULTATION_REPORT_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_REPORT_XML", GetXMLFromObject(consultationReport));
                if (consultationReport.FileData != null)
                {
                    Command.Parameters.AddWithValue("@FILE_DATA", consultationReport.FileData);
                }
                Command.Parameters.AddWithValue("@OPERATION", "ConsultationReport");
                if (consultationReport.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationReport.AddedBy.Value);
                }
                if (consultationReport.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationReport.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationReportResponse result = new ConsultationReportResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationReportResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationReport");

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

        public ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId)
        {
            try
            {
                Log.Info("Started call to GetConsultationReportList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationReportId = consultationReportId }));
                Command.CommandText = "SP_GET_REPORT_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationReportId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_REPORT_ID", consultationReportId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationReportResponse result = new ConsultationReportResponse();
                result.ConsultationReports = new List<ConsultationReportDisplay>();
                foreach (DataRow drConsultationReport in ds.Tables[0].Rows)
                {
                    result.ConsultationReports.Add(new ConsultationReportDisplay
                    {
                        Id = Convert.ToInt32(drConsultationReport["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationReport["ConsultationId"].ToString()),
                        FileName = drConsultationReport["FileName"] != DBNull.Value ? drConsultationReport["FileName"].ToString() : null,
                        FileData = drConsultationReport["FileData"] != DBNull.Value ? (byte[])drConsultationReport["FileData"] : null,
                        Description = drConsultationReport["Description"] != DBNull.Value ? drConsultationReport["Description"].ToString() : null,
                        DoctorName = drConsultationReport["DoctorName"] != DBNull.Value ? drConsultationReport["DoctorName"].ToString() : null,
                        DoctorPhoneNumber = drConsultationReport["DoctorPhoneNumber"] != DBNull.Value ? drConsultationReport["DoctorPhoneNumber"].ToString() : null,
                        ReportDate = drConsultationReport["ReportDate"] != DBNull.Value ? DateTime.Parse(drConsultationReport["ReportDate"].ToString()) : (DateTime?)null,
                        LabName = drConsultationReport["LabName"] != DBNull.Value ? drConsultationReport["LabName"].ToString() : null,
                        CountryId = int.Parse(drConsultationReport["CountryId"].ToString()),
                        Country = drConsultationReport["Country"] != DBNull.Value ? drConsultationReport["Country"].ToString() : null,
                        AddedBy = drConsultationReport["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationReport["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationReport["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationReport["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationReport["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationReport["ModifiedBy"].ToString()) : (int?) null,
                        ModifiedDate = drConsultationReport["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationReport["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationReportList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationSurgeryResponse InsertUpdateConsultationSurgery(ConsultationSurgeries consultationSurgery)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationSurgery");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationSurgery));
                Command.CommandText = "SP_CONSULTATION_SURGERY_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_SURGERY_XML", GetXMLFromObject(consultationSurgery));
                if (consultationSurgery.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationSurgery.AddedBy.Value);
                }
                if (consultationSurgery.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationSurgery.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationSurgeryResponse result = new ConsultationSurgeryResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationSurgeryResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationSurgery");

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

        public ConsultationSurgeryResponse GetConsultationSurgeryList(int consultationId, int? consultationSurgeryId)
        {
            try
            {
                Log.Info("Started call to GetConsultationSurgeryList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationSurgeryId = consultationSurgeryId }));
                Command.CommandText = "SP_GET_CONSULTATION_SURGERY_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationSurgeryId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_SURGERY_ID", consultationSurgeryId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationSurgeryResponse result = new ConsultationSurgeryResponse();
                result.ConsultationSurgeriesList = new List<ConsultationSurgeryDisplay>();
                foreach (DataRow drConsultationsurgery in ds.Tables[0].Rows)
                {
                    result.ConsultationSurgeriesList.Add(new ConsultationSurgeryDisplay
                    {
                        Id = Convert.ToInt32(drConsultationsurgery["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationsurgery["ConsultationId"].ToString()),
                        SurgeryId = Convert.ToInt32(drConsultationsurgery["SurgeryId"].ToString()),
                        SurgeryName = drConsultationsurgery["SurgeryName"] != DBNull.Value ? drConsultationsurgery["SurgeryName"].ToString() : null,
                        SurgeryDate = drConsultationsurgery["SurgeryDate"] != DBNull.Value ? DateTime.Parse(drConsultationsurgery["SurgeryDate"].ToString()) : (DateTime?)null,
                        AddedBy = drConsultationsurgery["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationsurgery["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationsurgery["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationsurgery["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationsurgery["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationsurgery["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationsurgery["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationsurgery["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationSurgeryList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationCancerTreatmentResponse InsertUpdateConsultationCancerTreatment(ConsultationCancerTreatments consultationCancerTreatment)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationCancerTreatment");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationCancerTreatment));
                Command.CommandText = "SP_CONSULTATION_CANCER_TREATMENT_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_CANCER_TREATMENT_XML", GetXMLFromObject(consultationCancerTreatment));
                if (consultationCancerTreatment.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationCancerTreatment.AddedBy.Value);
                }
                if (consultationCancerTreatment.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationCancerTreatment.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationCancerTreatmentResponse result = new ConsultationCancerTreatmentResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationCancerTreatmentResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationCancerTreatment");

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

        public ConsultationCancerTreatmentResponse GetConsultationCancerTreatmentList(int consultationId, int? consultationCancerTreatmentId)
        {
            try
            {
                Log.Info("Started call to GetConsultationCancerTreatmentList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationSurgeryId = consultationCancerTreatmentId }));
                Command.CommandText = "SP_GET_CONSULTATION_CANCER_TREATMENT_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationCancerTreatmentId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_CANCER_TREATMENT_ID", consultationCancerTreatmentId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationCancerTreatmentResponse result = new ConsultationCancerTreatmentResponse();
                result.ConsultationCancerTreatmentList = new List<ConsultationCancerTreatmentDisplay>();
                foreach (DataRow drConsultationCancerTreatment in ds.Tables[0].Rows)
                {
                    result.ConsultationCancerTreatmentList.Add(new ConsultationCancerTreatmentDisplay
                    {
                        Id = Convert.ToInt32(drConsultationCancerTreatment["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationCancerTreatment["ConsultationId"].ToString()),
                        CancerStageId = Convert.ToInt32(drConsultationCancerTreatment["CancerStageId"].ToString()),
                        CancerStage = drConsultationCancerTreatment["CancerStage"] != DBNull.Value ? drConsultationCancerTreatment["CancerStage"].ToString() : null,
                        CancerType = drConsultationCancerTreatment["CancerType"] != DBNull.Value ? drConsultationCancerTreatment["CancerType"].ToString() : null,
                        DignosisDate = drConsultationCancerTreatment["DignosisDate"] != DBNull.Value ? DateTime.Parse(drConsultationCancerTreatment["DignosisDate"].ToString()) : (DateTime?)null,
                        IsTreatmentOn = drConsultationCancerTreatment["IsTreatmentOn"] != DBNull.Value ? bool.Parse(drConsultationCancerTreatment["IsTreatmentOn"].ToString()) : false,
                        TreatmentType = drConsultationCancerTreatment["TreatmentType"] != DBNull.Value ? drConsultationCancerTreatment["TreatmentType"].ToString() : null,
                        TreatmentCompletionDate = drConsultationCancerTreatment["TreatmentCompletionDate"] != DBNull.Value ? DateTime.Parse(drConsultationCancerTreatment["TreatmentCompletionDate"].ToString()) : (DateTime?)null,
                        AddedBy = drConsultationCancerTreatment["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationCancerTreatment["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationCancerTreatment["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationCancerTreatment["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationCancerTreatment["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationCancerTreatment["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationCancerTreatment["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationCancerTreatment["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationCancerTreatmentList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationAllergyResponse InsertUpdateConsultationAllergy(ConsultationAllergies consultationAllergy)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationAllergy");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationAllergy));
                Command.CommandText = "SP_CONSULTATION_ALLERGY_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ALLERGY_XML", GetXMLFromObject(consultationAllergy));
                if (consultationAllergy.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationAllergy.AddedBy.Value);
                }
                if (consultationAllergy.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationAllergy.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationAllergyResponse result = new ConsultationAllergyResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationAllergyResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationAllergy");

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

        public ConsultationAllergyResponse GetConsultationAllergyList(int consultationId, int? consultationAllergyId)
        {
            try
            {
                Log.Info("Started call to GetConsultationAllergyList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationSurgeryId = consultationAllergyId }));
                Command.CommandText = "SP_GET_CONSULTATION_ALLERGY_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationAllergyId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_ALLERGY_ID", consultationAllergyId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationAllergyResponse result = new ConsultationAllergyResponse();
                result.ConsultationAllergyList = new List<ConsultationAllergyDisplay>();
                foreach (DataRow drConsultationAllergy in ds.Tables[0].Rows)
                {
                    result.ConsultationAllergyList.Add(new ConsultationAllergyDisplay
                    {
                        Id = Convert.ToInt32(drConsultationAllergy["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationAllergy["ConsultationId"].ToString()),
                        AllergyId = Convert.ToInt32(drConsultationAllergy["AllergyId"].ToString()),
                        AllergyName = drConsultationAllergy["AllergyName"] != DBNull.Value ? drConsultationAllergy["AllergyName"].ToString() : null,
                        AllergyStartDate = drConsultationAllergy["AllergyStartDate"] != DBNull.Value ? DateTime.Parse(drConsultationAllergy["AllergyStartDate"].ToString()) : (DateTime?)null,
                        Treatment = drConsultationAllergy["Treatment"] != DBNull.Value ? drConsultationAllergy["Treatment"].ToString() : null,
                        AddedBy = drConsultationAllergy["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationAllergy["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationAllergy["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationAllergy["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationAllergy["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationAllergy["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationAllergyList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationFamilyHistoryResponse InsertUpdateConsultationFamilyHistory(ConsultationFamilyHistory consultationFamilyHistory)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationFamilyHistory");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationFamilyHistory));
                Command.CommandText = "SP_CONSULTATION_FAMILY_HISTORY_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_FAMILY_HISTORY_XML", GetXMLFromObject(consultationFamilyHistory));
                if (consultationFamilyHistory.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationFamilyHistory.AddedBy.Value);
                }
                if (consultationFamilyHistory.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationFamilyHistory.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationFamilyHistoryResponse result = new ConsultationFamilyHistoryResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationFamilyHistoryResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationFamilyHistory");

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

        public ConsultationFamilyHistoryResponse GetConsultationFamilyHistoryList(int consultationId, int? consultationFamilyHistoryId
            , int? relationshipId, bool? excludeSelf)
        {
            try
            {
                Log.Info("Started call to GetConsultationFamilyHistoryList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new
                    {
                      consultationId = consultationId
                    , consultationSurgeryId = consultationFamilyHistoryId
                    , relationshipId = relationshipId
                    , excludeSelf = excludeSelf
                }));
                Command.CommandText = "SP_GET_CONSULTATION_FAMILY_HISTORY_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationFamilyHistoryId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_FAMILY_HISTORY_ID", consultationFamilyHistoryId);
                }
                if (relationshipId.HasValue)
                {
                    Command.Parameters.AddWithValue("@RELATIONSHIP_ID", relationshipId);
                }
                if (excludeSelf.HasValue)
                {
                    Command.Parameters.AddWithValue("@EXCLUDE_SELF", excludeSelf);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationFamilyHistoryResponse result = new ConsultationFamilyHistoryResponse();
                result.ConsultationFamilyHistories = new List<ConsultationFamilyHistoryDisplay>();
                foreach (DataRow drConsultationAllergy in ds.Tables[0].Rows)
                {
                    result.ConsultationFamilyHistories.Add(new ConsultationFamilyHistoryDisplay
                    {
                        Id = Convert.ToInt32(drConsultationAllergy["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationAllergy["ConsultationId"].ToString()),
                        RelationshipId = Convert.ToInt32(drConsultationAllergy["RelationshipId"].ToString()),
                        Relationship = drConsultationAllergy["Relationship"] != DBNull.Value ? drConsultationAllergy["Relationship"].ToString() : null,
                        HealthConditionId = Convert.ToInt32(drConsultationAllergy["HealthConditionId"].ToString()),
                        HealthCondition = drConsultationAllergy["HealthCondition"] != DBNull.Value ? drConsultationAllergy["HealthCondition"].ToString() : null,
                        CurrentAge = drConsultationAllergy["CurrentAge"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["CurrentAge"].ToString()) : (int?)null,
                        AgeOnConditionStart = drConsultationAllergy["AgeOnConditionStart"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["AgeOnConditionStart"].ToString()) : (int?)null,
                        AgeOnDeath = drConsultationAllergy["AgeOnDeath"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["AgeOnDeath"].ToString()) : (int?)null,
                        CauseOfDeath = drConsultationAllergy["CauseOfDeath"] != DBNull.Value ? drConsultationAllergy["CauseOfDeath"].ToString() : null,
                        IsAlive = drConsultationAllergy["IsAlive"] != DBNull.Value ? Convert.ToBoolean(drConsultationAllergy["IsAlive"].ToString()) : true,
                        AddedBy = drConsultationAllergy["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationAllergy["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationAllergy["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationAllergy["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationAllergy["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationAllergy["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationAllergy["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationFamilyHistoryList result " + JsonConvert.SerializeObject(result));

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

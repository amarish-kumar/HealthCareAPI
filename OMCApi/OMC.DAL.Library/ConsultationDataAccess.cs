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
                            PackageId = Convert.ToInt32(reader["PackageId"].ToString()),
                            PackageName = reader["PackageName"] != DBNull.Value ? reader["PackageName"].ToString() : null,
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
                            PackageName = resultRow["PackageName"] != DBNull.Value ? resultRow["PackageName"].ToString() : null,
                            PackageId = Convert.ToInt32(resultRow["PackageId"].ToString()),
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
                        OtherDescription = drConsultationsurgery["OtherDescription"] != DBNull.Value ? drConsultationsurgery["OtherDescription"].ToString() : null,
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
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationCancerTreatmentId = consultationCancerTreatmentId }));
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

        public ConsultationIllegalDrugDetailsResponse InsertUpdateConsultationIllegalDrugDetail(ConsultationIllegalDrugDetails consultationIllegalDrugDetails)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationIllegalDrugDetail");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationIllegalDrugDetails));
                Command.CommandText = "SP_CONSULTATION_ILLEGALDRUG_DETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ILLEGALDRUG_DETAILS_XML", GetXMLFromObject(consultationIllegalDrugDetails));
                if (consultationIllegalDrugDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationIllegalDrugDetails.AddedBy.Value);
                }
                if (consultationIllegalDrugDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationIllegalDrugDetails.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationIllegalDrugDetailsResponse result = new ConsultationIllegalDrugDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationIllegalDrugDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationIllegalDrugDetail");

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

        public ConsultationIllegalDrugDetailsResponse GetConsultationIllegalDrugDetailList(int consultationId, int? consultationIllegalDrugDetailsId)
        {
            try
            {
                Log.Info("Started call to GetConsultationIllegalDrugDetailList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationIllegalDrugDetailsId = consultationIllegalDrugDetailsId }));
                Command.CommandText = "SP_CONSULTATION_ILLEGALDRUG_DETAILS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationIllegalDrugDetailsId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_ILLEGALDRUGDETAILS_ID", consultationIllegalDrugDetailsId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationIllegalDrugDetailsResponse result = new ConsultationIllegalDrugDetailsResponse();
                result.ConsultationIllegalDrugDetailsDisplayList = new List<ConsultationIllegalDrugDetailsDisplay>();
                foreach (DataRow drConsultationIllegalDrugDetails in ds.Tables[0].Rows)
                {
                    result.ConsultationIllegalDrugDetailsDisplayList.Add(new ConsultationIllegalDrugDetailsDisplay
                    {
                        Id = Convert.ToInt32(drConsultationIllegalDrugDetails["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationIllegalDrugDetails["ConsultationId"].ToString()),
                        ConsumeDrugs = drConsultationIllegalDrugDetails["ConsumeDrugs"] != DBNull.Value ? bool.Parse(drConsultationIllegalDrugDetails["ConsumeDrugs"].ToString()) : false,
                        IllegalDrugsID = drConsultationIllegalDrugDetails["IllegalDrugsID"] != DBNull.Value ? Convert.ToInt32(drConsultationIllegalDrugDetails["IllegalDrugsID"].ToString()) : (int?)null,
                        IllegalDrugDesc = drConsultationIllegalDrugDetails["DrugName"] != DBNull.Value ? drConsultationIllegalDrugDetails["DrugName"].ToString() : null,
                        OtherDescription = drConsultationIllegalDrugDetails["OtherDescription"] != DBNull.Value ? drConsultationIllegalDrugDetails["OtherDescription"].ToString() : null,
                        Frequency = drConsultationIllegalDrugDetails["Frequency"] != DBNull.Value ? drConsultationIllegalDrugDetails["Frequency"].ToString() : null,
                        PerFrequency = drConsultationIllegalDrugDetails["PerFrequency"] != DBNull.Value ? Convert.ToInt32(drConsultationIllegalDrugDetails["PerFrequency"].ToString()) : (int?)null,
                        AddedBy = drConsultationIllegalDrugDetails["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationIllegalDrugDetails["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationIllegalDrugDetails["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationIllegalDrugDetails["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationIllegalDrugDetails["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationIllegalDrugDetails["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationIllegalDrugDetails["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationIllegalDrugDetails["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationIllegalDrugDetailList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationPregnancyDetailsResponse InsertUpdateConsultationPregnancyDetail(ConsultationPregnancyDetails consultationPregnancyDetails)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationPregnancyDetail");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationPregnancyDetails));
                Command.CommandText = "SP_CONSULTATION_PREGNANCYDETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                if ((consultationPregnancyDetails.MCSymptomIDArray) != null && consultationPregnancyDetails.MCSymptomIDArray.Length > 0)
                { consultationPregnancyDetails.MCSymptomID = string.Join(",", consultationPregnancyDetails.MCSymptomIDArray); }

                Command.Parameters.AddWithValue("@CONSULTATION_PREGNANCYDETAILS_XML", GetXMLFromObject(consultationPregnancyDetails));
                if (consultationPregnancyDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationPregnancyDetails.AddedBy.Value);
                }
                if (consultationPregnancyDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationPregnancyDetails.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationPregnancyDetailsResponse result = new ConsultationPregnancyDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationPregnancyDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationPregnancyDetail");

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

        public ConsultationPregnancyDetailsResponse GetConsultationPregnancyDetailsList(int consultationId, int? consultationPregnancyDetailsId)
        {
            try
            {
                Log.Info("Started call to GetConsultationPregnancyDetailsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationPregnancyDetailsId = consultationPregnancyDetailsId }));
                Command.CommandText = "SP_CONSULTATION_PREGNANCYDETAILS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationPregnancyDetailsId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_PREGNANCYDETAILS_ID", consultationPregnancyDetailsId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationPregnancyDetailsResponse result = new ConsultationPregnancyDetailsResponse();
                result.ConsultationPregnancyDetailsList = new List<ConsultationPregnancyDetailsDisplay>();
                foreach (DataRow drConsultationPregnancyDetails in ds.Tables[0].Rows)
                {
                    result.ConsultationPregnancyDetailsList.Add(new ConsultationPregnancyDetailsDisplay
                    {
                        Id = Convert.ToInt32(drConsultationPregnancyDetails["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationPregnancyDetails["ConsultationId"].ToString()),
                        CurrentlyPregnant = drConsultationPregnancyDetails["CurrentlyPregnant"] != DBNull.Value ? bool.Parse(drConsultationPregnancyDetails["CurrentlyPregnant"].ToString()) : false,
                        CurrentPregnancyMonths = drConsultationPregnancyDetails["CurrentPregnancyMonths"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["CurrentPregnancyMonths"].ToString()) : (int?)null,
                        CurrentPregnancyEDD = drConsultationPregnancyDetails["CurrentPregnancyEDD"] != DBNull.Value ? DateTime.Parse(drConsultationPregnancyDetails["CurrentPregnancyEDD"].ToString()) : (DateTime?)null,
                        PregnantBefore = drConsultationPregnancyDetails["PregnantBefore"] != DBNull.Value ? bool.Parse(drConsultationPregnancyDetails["PregnantBefore"].ToString()) : false,
                        MenstrualCycles = drConsultationPregnancyDetails["MenstrualCycles"] != DBNull.Value ? bool.Parse(drConsultationPregnancyDetails["MenstrualCycles"].ToString()) : false,
                        NoMCReason = drConsultationPregnancyDetails["NoMCReason"] != DBNull.Value ? drConsultationPregnancyDetails["NoMCReason"].ToString() : null,
                        LastMCCycle = drConsultationPregnancyDetails["LastMCCycle"] != DBNull.Value ? DateTime.Parse(drConsultationPregnancyDetails["LastMCCycle"].ToString()) : (DateTime?)null,
                        MCRegInterval = drConsultationPregnancyDetails["MCRegInterval"] != DBNull.Value ? bool.Parse(drConsultationPregnancyDetails["MCRegInterval"].ToString()) : false,
                        LenMCCycle = drConsultationPregnancyDetails["LenMCCycle"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["LenMCCycle"].ToString()) : (int?)null,
                        MCStartAge = drConsultationPregnancyDetails["MCStartAge"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["MCStartAge"].ToString()) : (int?)null,
                        MCFlow = drConsultationPregnancyDetails["MCFlow"] != DBNull.Value ? drConsultationPregnancyDetails["MCFlow"].ToString() : null,
                        MCProductType = drConsultationPregnancyDetails["MCProductType"] != DBNull.Value ? drConsultationPregnancyDetails["MCProductType"].ToString() : null,
                        MCProductPerDay = drConsultationPregnancyDetails["MCProductPerDay"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["MCProductPerDay"].ToString()) : (int?)null,
                        MCPain = drConsultationPregnancyDetails["MCPain"] != DBNull.Value ? bool.Parse(drConsultationPregnancyDetails["MCPain"].ToString()) : false,
                        MCPainSeverity = drConsultationPregnancyDetails["MCPainSeverity"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["MCPainSeverity"].ToString()) : (int?)null,
                        MCSymptomDesc = drConsultationPregnancyDetails["MCSymptomDesc"] != DBNull.Value ? drConsultationPregnancyDetails["MCSymptomDesc"].ToString() : null,
                        AddedBy = drConsultationPregnancyDetails["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationPregnancyDetails["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationPregnancyDetails["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationPregnancyDetails["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationPregnancyDetails["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationPregnancyDetails["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationPregnancyDetails["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationPregnancyDetailsList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationPreviousPregnancyDetailsResponse InsertUpdateConsultationPreviousPregnancyDetail(ConsultationPreviousPregnancyDetails consultationPreviousPregnancyDetails)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationPreviousPregnancyDetail");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationPreviousPregnancyDetails));
                Command.CommandText = "SP_CONSULTATION_PREVIOUSPREGNANCYDETAILS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                
                Command.Parameters.AddWithValue("@CONSULTATION_PREVIOUSPREGNANCYDETAILS_XML", GetXMLFromObject(consultationPreviousPregnancyDetails));
                if (consultationPreviousPregnancyDetails.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationPreviousPregnancyDetails.AddedBy.Value);
                }
                if (consultationPreviousPregnancyDetails.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationPreviousPregnancyDetails.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationPreviousPregnancyDetailsResponse result = new ConsultationPreviousPregnancyDetailsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationPreviousPregnancyDetailsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationPreviousPregnancyDetail");

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

        public ConsultationPreviousPregnancyDetailsResponse GetConsultationPreviousPregnancyDetailsList(int consultationId, int? consultationPreviousPregnancyDetailsId, int? CurrentPregnancyID)
        {
            try
            {
                Log.Info("Started call to GetConsultationPreviousPregnancyDetailsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationPreviousPregnancyDetailsId = consultationPreviousPregnancyDetailsId, CurrentPregnancyID = CurrentPregnancyID }));
                Command.CommandText = "SP_CONSULTATION_PREVIOUSPREGNANCYDETAILS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationPreviousPregnancyDetailsId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_PREVIOUSPREGNANCYDETAILS_ID", consultationPreviousPregnancyDetailsId);
                }
                if (CurrentPregnancyID.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_CURRENTPREGNANCYDETAILS_ID", CurrentPregnancyID);
                }
                //Command.Parameters.AddWithValue("@CONSULTATION_CURRENTPREGNANCYDETAILS_ID", consultationId);
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationPreviousPregnancyDetailsResponse result = new ConsultationPreviousPregnancyDetailsResponse();
                result.ConsultationPreviousPregnancyDetailsList = new List<ConsultationPreviousPregnancyDetailsDisplay>();
                foreach (DataRow drConsultationPreviousPregnancyDetails in ds.Tables[0].Rows)
                {
                    result.ConsultationPreviousPregnancyDetailsList.Add(new ConsultationPreviousPregnancyDetailsDisplay
                    {
                        Id = Convert.ToInt32(drConsultationPreviousPregnancyDetails["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationPreviousPregnancyDetails["ConsultationId"].ToString()),
                        CurrentPregnancyID = Convert.ToInt32(drConsultationPreviousPregnancyDetails["CurrentPregnancyID"].ToString()),
                        NoofPregnancy = drConsultationPreviousPregnancyDetails["NoofPregnancy"] != DBNull.Value ? Convert.ToInt32(drConsultationPreviousPregnancyDetails["NoofPregnancy"].ToString()) : (int?)null,
                        ChildNo = drConsultationPreviousPregnancyDetails["ChildNo"] != DBNull.Value ? Convert.ToInt32(drConsultationPreviousPregnancyDetails["ChildNo"].ToString()) : (int?)null,
                        DeliveryYear = drConsultationPreviousPregnancyDetails["DeliveryYear"] != DBNull.Value ? drConsultationPreviousPregnancyDetails["DeliveryYear"].ToString() : null,
                        DeliveryType = drConsultationPreviousPregnancyDetails["DeliveryType"] != DBNull.Value ? drConsultationPreviousPregnancyDetails["DeliveryType"].ToString() : null,
                        AddedBy = drConsultationPreviousPregnancyDetails["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationPreviousPregnancyDetails["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationPreviousPregnancyDetails["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationPreviousPregnancyDetails["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationPreviousPregnancyDetails["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationPreviousPregnancyDetails["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationPreviousPregnancyDetails["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationPreviousPregnancyDetails["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationPreviousPregnancyDetailsList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationSDDHabitsResponse InsertUpdateConsultationSDDHabits(ConsultationSDDHabits consultationSDDHabits)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationSDDHabits");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationSDDHabits));
                Command.CommandText = "SP_CONSULTATION_SDDHABITS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_SDDHABITS_XML", GetXMLFromObject(consultationSDDHabits));
                if (consultationSDDHabits.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationSDDHabits.AddedBy.Value);
                }
                if (consultationSDDHabits.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationSDDHabits.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationSDDHabitsResponse result = new ConsultationSDDHabitsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationSDDHabitsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationSDDHabits");

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

        public ConsultationSDDHabitsResponse GetConsultationSDDHabitsList(int consultationId, int? consultationSDDHabitsId)
        {
            try
            {
                Log.Info("Started call to GetConsultationSDDHabitsList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationSDDHabitsId = consultationSDDHabitsId }));
                Command.CommandText = "SP_CONSULTATION_SDDHABITS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationSDDHabitsId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_SDDHABITS_ID", consultationSDDHabitsId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationSDDHabitsResponse result = new ConsultationSDDHabitsResponse();
                result.ConsultationSDDHabitsDisplayList = new List<ConsultationSDDHabitsDisplay>();
                foreach (DataRow drConsultationSDDHabitsDisplay in ds.Tables[0].Rows)
                {
                    result.ConsultationSDDHabitsDisplayList.Add(new ConsultationSDDHabitsDisplay
                    {
                        Id = Convert.ToInt32(drConsultationSDDHabitsDisplay["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationSDDHabitsDisplay["ConsultationId"].ToString()),
                        DoSmoke = drConsultationSDDHabitsDisplay["DoSmoke"] != DBNull.Value ? bool.Parse(drConsultationSDDHabitsDisplay["DoSmoke"].ToString()) : false,
                        EverSmoked = drConsultationSDDHabitsDisplay["EverSmoked"] != DBNull.Value ? bool.Parse(drConsultationSDDHabitsDisplay["EverSmoked"].ToString()) : false,
                        YearOfQuittingSmoking = drConsultationSDDHabitsDisplay["YearOfQuittingSmoking"] != DBNull.Value ? Convert.ToInt32(drConsultationSDDHabitsDisplay["YearOfQuittingSmoking"].ToString()) : (int?)null,
                        SmokingFreq = drConsultationSDDHabitsDisplay["SmokingFreq"] != DBNull.Value ? Convert.ToInt32(drConsultationSDDHabitsDisplay["SmokingFreq"].ToString()) : (int?)null,
                        AddedBy = drConsultationSDDHabitsDisplay["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationSDDHabitsDisplay["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationSDDHabitsDisplay["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationSDDHabitsDisplay["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationSDDHabitsDisplay["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationSDDHabitsDisplay["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationSDDHabitsDisplay["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationSDDHabitsDisplay["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationSDDHabitsList result " + JsonConvert.SerializeObject(result));

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
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationAllergyId = consultationAllergyId }));
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
                        OtherDescription = drConsultationAllergy["OtherDescription"] != DBNull.Value ? drConsultationAllergy["OtherDescription"].ToString() : null,
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
                    , consultationFamilyHistoryId = consultationFamilyHistoryId
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
                foreach (DataRow drConsultationFamilyHistory in ds.Tables[0].Rows)
                {
                    result.ConsultationFamilyHistories.Add(new ConsultationFamilyHistoryDisplay
                    {
                        Id = Convert.ToInt32(drConsultationFamilyHistory["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationFamilyHistory["ConsultationId"].ToString()),
                        RelationshipId = Convert.ToInt32(drConsultationFamilyHistory["RelationshipId"].ToString()),
                        Relationship = drConsultationFamilyHistory["Relationship"] != DBNull.Value ? drConsultationFamilyHistory["Relationship"].ToString() : null,
                        HealthConditionId = Convert.ToInt32(drConsultationFamilyHistory["HealthConditionId"].ToString()),
                        HealthCondition = drConsultationFamilyHistory["HealthCondition"] != DBNull.Value ? drConsultationFamilyHistory["HealthCondition"].ToString() : null,
                        OtherHealthConditionDescription = drConsultationFamilyHistory["OtherHealthConditionDescription"] != DBNull.Value ? drConsultationFamilyHistory["OtherHealthConditionDescription"].ToString() : null,
                        CurrentAge = drConsultationFamilyHistory["CurrentAge"] != DBNull.Value ? Convert.ToInt32(drConsultationFamilyHistory["CurrentAge"].ToString()) : (int?)null,
                        ConditionStartDate = drConsultationFamilyHistory["ConditionStartDate"] != DBNull.Value ? Convert.ToDateTime(drConsultationFamilyHistory["ConditionStartDate"].ToString()) : (DateTime?)null,
                        AgeOnConditionStart = drConsultationFamilyHistory["AgeOnConditionStart"] != DBNull.Value ? Convert.ToInt32(drConsultationFamilyHistory["AgeOnConditionStart"].ToString()) : (int?)null,
                        AgeOnDeath = drConsultationFamilyHistory["AgeOnDeath"] != DBNull.Value ? Convert.ToInt32(drConsultationFamilyHistory["AgeOnDeath"].ToString()) : (int?)null,
                        CauseOfDeath = drConsultationFamilyHistory["CauseOfDeath"] != DBNull.Value ? drConsultationFamilyHistory["CauseOfDeath"].ToString() : null,
                        IsAlive = drConsultationFamilyHistory["IsAlive"] != DBNull.Value ? Convert.ToBoolean(drConsultationFamilyHistory["IsAlive"].ToString()) : true,
                        AddedBy = drConsultationFamilyHistory["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationFamilyHistory["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationFamilyHistory["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationFamilyHistory["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationFamilyHistory["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationFamilyHistory["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationFamilyHistory["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationFamilyHistory["ModifiedDate"].ToString()) : (DateTime?)null,
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

        public ConsultationOccupationResponse InsertUpdateConsultationOccupation(ConsultationOccupation consultationOccupation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationOccupation");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationOccupation));
                Command.CommandText = "SP_CONSULTATION_OCCUPATION_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_OCCUPATION_XML", GetXMLFromObject(consultationOccupation));
                if (consultationOccupation.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationOccupation.AddedBy.Value);
                }
                if (consultationOccupation.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationOccupation.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationOccupationResponse result = new ConsultationOccupationResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationOccupationResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationOccupation");

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

        public ConsultationOccupationResponse GetConsultationOccupationList(int consultationId, int? consultationOccupationId)
        {
            try
            {
                Log.Info("Started call to GetConsultationOccupationList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationOccupationId = consultationOccupationId }));
                Command.CommandText = "SP_GET_CONSULTATION_OCCUPATION_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationOccupationId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_OCCUPATION_ID", consultationOccupationId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationOccupationResponse result = new ConsultationOccupationResponse();
                result.ConsultationOccupationList = new List<ConsultationOccupationDisplay>();
                foreach (DataRow drConsultationOccupation in ds.Tables[0].Rows)
                {
                    result.ConsultationOccupationList.Add(new ConsultationOccupationDisplay
                    {
                        Id = Convert.ToInt32(drConsultationOccupation["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationOccupation["ConsultationId"].ToString()),
                        OccupationId = Convert.ToInt32(drConsultationOccupation["OccupationId"].ToString()),
                        OccupationName = drConsultationOccupation["OccupationName"] != DBNull.Value ? drConsultationOccupation["OccupationName"].ToString() : null,
                        OtherDescription = drConsultationOccupation["OtherDescription"] != DBNull.Value ? drConsultationOccupation["OtherDescription"].ToString() : null,
                        AddedBy = drConsultationOccupation["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationOccupation["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationOccupation["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationOccupation["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationOccupation["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationOccupation["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationOccupation["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationOccupation["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationOccupationList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationBloodPressureReadingResponse InsertUpdateConsultationBloodPressureReading(ConsultationBloodPressureReading consultationBloodPressureReading)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationBloodPressureReading");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationBloodPressureReading));
                Command.CommandText = "SP_CONSULTATION_BLOOD_PRESSURE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_BLOOD_PRESSURE_XML", GetXMLFromObject(consultationBloodPressureReading));
                if (consultationBloodPressureReading.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationBloodPressureReading.AddedBy.Value);
                }
                if (consultationBloodPressureReading.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationBloodPressureReading.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationBloodPressureReadingResponse result = new ConsultationBloodPressureReadingResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationBloodPressureReadingResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationBloodPressureReading");

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

        public ConsultationBloodPressureReadingResponse GetConsultationBloodPressureReadingList(int consultationId, int? consultationBloodPressureReadingId)
        {
            try
            {
                Log.Info("Started call to GetConsultationBloodPressureReadingList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationBloodPressureReadingId = consultationBloodPressureReadingId }));
                Command.CommandText = "SP_GET_CONSULTATION_BLOOD_PRESSURE_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationBloodPressureReadingId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_BLOOD_PRESSURE_ID", consultationBloodPressureReadingId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationBloodPressureReadingResponse result = new ConsultationBloodPressureReadingResponse();
                result.ConsultationBloodPressureReadingList = new List<ConsultationBloodPressureReading>();
                foreach (DataRow drConsultationBloodPressureReading in ds.Tables[0].Rows)
                {
                    result.ConsultationBloodPressureReadingList.Add(new ConsultationBloodPressureReading
                    {
                        Id = Convert.ToInt32(drConsultationBloodPressureReading["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationBloodPressureReading["ConsultationId"].ToString()),
                        Systolic = Convert.ToInt32(drConsultationBloodPressureReading["Systolic"].ToString()),
                        Diastolic = Convert.ToInt32(drConsultationBloodPressureReading["Diastolic"].ToString()),
                        Timestamp = drConsultationBloodPressureReading["Timestamp"] != DBNull.Value ? DateTime.Parse(drConsultationBloodPressureReading["Timestamp"].ToString()) : (DateTime?)null,
                        AddedBy = drConsultationBloodPressureReading["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationBloodPressureReading["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationBloodPressureReading["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationBloodPressureReading["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationBloodPressureReading["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationBloodPressureReading["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationBloodPressureReading["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationBloodPressureReading["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationBloodPressureReadingList result " + JsonConvert.SerializeObject(result));

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

        public ConsultationMedicationResponse InsertUpdateConsultationMedication(ConsultationMedications consultationMedication)
        {
            try
            {
                Log.Info("Started call to InsertUpdateConsultationMedication");
                Log.Info("parameter values" + JsonConvert.SerializeObject(consultationMedication));
                Command.CommandText = "SP_CONSULTATION_MEDICATION_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_MEDICATION_XML", GetXMLFromObject(consultationMedication));
                if (consultationMedication.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationMedication.AddedBy.Value);
                }
                if (consultationMedication.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", consultationMedication.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                ConsultationMedicationResponse result = new ConsultationMedicationResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ConsultationMedicationResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateConsultationMedication");

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

        public ConsultationMedicationResponse GetConsultationMedicationList(int consultationId, int? consultationMedicationId)
        {
            try
            {
                Log.Info("Started call to GetConsultationMedicationList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { consultationId = consultationId, consultationMedicationId = consultationMedicationId }));
                Command.CommandText = "SP_GET_CONSULTATION_MEDICATION_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@CONSULTATION_ID", consultationId);
                if (consultationMedicationId.HasValue)
                {
                    Command.Parameters.AddWithValue("@CONSULTATION_MEDICATION_ID", consultationMedicationId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ConsultationMedicationResponse result = new ConsultationMedicationResponse();
                result.ConsultationMedicationList = new List<ConsultationMedicationDisplay>();
                foreach (DataRow drConsultationMedication in ds.Tables[0].Rows)
                {
                    result.ConsultationMedicationList.Add(new ConsultationMedicationDisplay
                    {
                        Id = Convert.ToInt32(drConsultationMedication["Id"].ToString()),
                        ConsultationId = Convert.ToInt32(drConsultationMedication["ConsultationId"].ToString()),
                        DrugBrandId = Convert.ToInt32(drConsultationMedication["DrugBrandId"].ToString()),
                        DrugBrandName = drConsultationMedication["DrugBrandName"] != DBNull.Value ? drConsultationMedication["DrugBrandName"].ToString() : string.Empty,
                        DrugBrandOtherDescription = drConsultationMedication["DrugBrandOtherDescription"] != DBNull.Value ? drConsultationMedication["DrugBrandOtherDescription"].ToString() : string.Empty,
                        DrugChemicalId = Convert.ToInt32(drConsultationMedication["DrugChemicalId"].ToString()),
                        DrugChemicalName = drConsultationMedication["DrugChemicalName"] != DBNull.Value ? drConsultationMedication["DrugChemicalName"].ToString() : string.Empty,
                        DrugChemicalOtherDescription = drConsultationMedication["DrugChemicalOtherDescription"] != DBNull.Value ? drConsultationMedication["DrugChemicalOtherDescription"].ToString() : string.Empty,
                        DrugFrequencyId = Convert.ToInt32(drConsultationMedication["DrugFrequencyId"].ToString()),
                        DrugFrequencyName = drConsultationMedication["DrugFrequencyName"] != DBNull.Value ? drConsultationMedication["DrugFrequencyName"].ToString() : string.Empty,
                        DrugTypeId = Convert.ToInt32(drConsultationMedication["DrugTypeId"].ToString()),
                        DrugTypeName = drConsultationMedication["DrugTypeName"] != DBNull.Value ? drConsultationMedication["DrugTypeName"].ToString() : string.Empty,
                        DrugSubTypeId = Convert.ToInt32(drConsultationMedication["DrugSubTypeId"].ToString()),
                        DrugSubTypeName = drConsultationMedication["DrugSubTypeName"] != DBNull.Value ? drConsultationMedication["DrugSubTypeName"].ToString() : string.Empty,
                        DrugStartDate = drConsultationMedication["DrugStartDate"] != DBNull.Value ? Convert.ToDateTime(drConsultationMedication["DrugStartDate"].ToString()) : (DateTime?)null,
                        DrugEndDate = drConsultationMedication["DrugEndDate"] != DBNull.Value ? Convert.ToDateTime(drConsultationMedication["DrugEndDate"].ToString()) : (DateTime?)null,
                        DrugDosage = drConsultationMedication["DrugDosage"] != DBNull.Value ? Decimal.Parse(drConsultationMedication["DrugDosage"].ToString()) : (Decimal?)null,
                        DrugUnitId = Convert.ToInt32(drConsultationMedication["DrugUnitId"].ToString()),
                        DrugUnitName = drConsultationMedication["DrugUnitName"] != DBNull.Value ? drConsultationMedication["DrugUnitName"].ToString() : string.Empty,
                        AddedBy = drConsultationMedication["AddedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationMedication["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drConsultationMedication["AddedDate"] != DBNull.Value ? DateTime.Parse(drConsultationMedication["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drConsultationMedication["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drConsultationMedication["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drConsultationMedication["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drConsultationMedication["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetConsultationMedicationList result " + JsonConvert.SerializeObject(result));

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

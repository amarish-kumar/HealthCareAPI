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

        #endregion        
    }
}

using OMC.BL.Interface;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Library
{
    public class ConsultationBL : IConsultationBL
    {
        #region Declarations
        IConsultationDataAccess _consultationDA;
        #endregion

        #region Constructors

        public ConsultationBL(IConsultationDataAccess ConsultationDA)
        {
            this._consultationDA = ConsultationDA;
        }

        #endregion

        #region Methods
        public List<ConsultationDisplay> GetConsultationList(int userId, string userRole)
        {
            try
            {
                return this._consultationDA.GetConsultationList(userId, userRole);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConversationResponse GetConversationList(int consultationId, int userId, string userRole)
        {
            try
            {
                return this._consultationDA.GetConversationList(consultationId, userId, userRole);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationResponse InitiateConsultation(Consultation consultationDetails)
        {
            try
            {
                return this._consultationDA.InitiateConsultation(consultationDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }


        public PatientEnquiryResponse UnregisteredPatientEnquiry(PatientEnquiry enquiry)
        {
            try
            {
                return this._consultationDA.UnregisteredPatientEnquiry(enquiry);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConversationResponse RecordConversation(Conversation conversationDetails)
        {
            try
            {
                return this._consultationDA.RecordConversation(conversationDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationReportResponse InsertUpdateConsultationReport(ConsultationReports consultationReport)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationReport(consultationReport);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId)
        {
            try
            {
                return this._consultationDA.GetConsultationReportList(consultationId, consultationReportId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }
        #endregion

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

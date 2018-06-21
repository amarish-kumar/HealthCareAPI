using OMC.BL.Interface;
using OMC.DAL.Interface;
using OMC.Models;
using System;

namespace OMC.BL.Library
{
    public class Doctor : IDoctor
    {
        #region Declarations

        IDoctorDataAccess _doctorDA;

        #endregion

        #region Constructors

        public Doctor(IDoctorDataAccess DoctorDA)
        {
            this._doctorDA = DoctorDA;
        }

        #endregion

        #region Methods

        public DoctorProfileResponse InsertUpdateDoctorProfile(DoctorProfile doctorProfile, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorProfile(doctorProfile, operation);
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
    }
}

using OMC.BL.Interface;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Library
{
    public class DoctorBL : IDoctorBL
    {
        #region Declarations

        IDoctorDataAccess _doctorDA;

        #endregion

        #region Constructors

        public DoctorBL(IDoctorDataAccess DoctorDA)
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

        public List<DoctorProfileDisplay> GetDoctorProfileList(int userId, int? doctorId)
        {
            try
            {
                return this._doctorDA.GetDoctorProfileList(userId, doctorId);
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

        public DoctorProfileResponse DeleteDoctorProfile(int doctorProfileId, int userId)
        {
            try
            {
                return this._doctorDA.DeleteDoctorProfile(doctorProfileId, userId);
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

        public UserAddressResponse InsertUpdateUserAddress(UserAddress userAddress, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateUserAddress(userAddress, operation);
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

        public UserAddressResponse GetUserAddressList(int userId, int? addressId)
        {
            try
            {
                return this._doctorDA.GetUserAddressList(userId, addressId);
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

    }

    #endregion
}

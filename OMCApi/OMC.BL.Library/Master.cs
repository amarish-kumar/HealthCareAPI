using OMC.BL.Interface;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Library
{
    public class Master : IMaster
    {
        #region Declarations
        IMasterDataAccess _masterDA;
        #endregion

        #region Constructors

        public Master(IMasterDataAccess MasterDA)
        {
            this._masterDA = MasterDA;
        }

        #endregion

        #region Methods
        public List<Country> GetCountries(bool? isActive)
        {
            try
            {
                return this._masterDA.GetCountries(isActive);
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

        public List<Address> GetAddressTypes(bool? isActive)
        {
            try
            {
                return this._masterDA.GetAddressTypes(isActive);
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

        public List<Role> GetRoles(bool? isActive, string roleDescription)
        {
            try
            {
                return this._masterDA.GetRoles(isActive, roleDescription);
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

        public List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description)
        {
            try
            {
                return this._masterDA.GetConsultationStatuses(isActive, description);
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

        public List<UserDetail> GetUserList(bool? isActive, string userRole)
        {
            try
            {
                return this._masterDA.GetUserList(isActive, userRole);
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

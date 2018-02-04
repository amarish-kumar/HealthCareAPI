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

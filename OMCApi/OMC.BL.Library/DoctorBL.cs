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

        public DoctorAwardsResponse InsertUpdateDoctorAward(DoctorAwards doctorAward, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorAward(doctorAward, operation);
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

        public DoctorAwardsResponse GetDoctorAwardList(int doctorId, int? doctorAwardId)
        {
            try
            {
                return this._doctorDA.GetDoctorAwardList(doctorId, doctorAwardId);
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

        public DoctorBoardResponse InsertUpdateDoctorBoard(DoctorBoard doctorBoard, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorBoard(doctorBoard, operation);
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

        public DoctorBoardResponse GetDoctorBoardList(int doctorId, int? doctorBoardId)
        {
            try
            {
                return this._doctorDA.GetDoctorBoardList(doctorId, doctorBoardId);
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

        public DoctorEducationResponse InsertUpdateDoctorEducation(DoctorEducation doctorEducation, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorEducation(doctorEducation, operation);
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

        public DoctorEducationResponse GetDoctorEducationList(int doctorId, int? doctorEducationId)
        {
            try
            {
                return this._doctorDA.GetDoctorEducationList(doctorId, doctorEducationId);
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

        public DoctorFellowshipResponse InsertUpdateDoctorFellowship(DoctorFellowship doctorFellowship, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorFellowship(doctorFellowship, operation);
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

        public DoctorFellowshipResponse GetDoctorFellowshipList(int doctorId, int? doctorFellowshipId)
        {
            try
            {
                return this._doctorDA.GetDoctorFellowshipList(doctorId, doctorFellowshipId);
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

        public DoctorImagesResponse InsertUpdateDoctorImage(DoctorImages doctorImage, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorImage(doctorImage, operation);
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

        public DoctorImagesResponse GetDoctorImageList(int doctorId, int? doctorImageId)
        {
            try
            {
                return this._doctorDA.GetDoctorImageList(doctorId, doctorImageId);
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

        public DoctorResidencyResponse InsertUpdateDoctorResidency(DoctorResidency doctorResidency, string operation)
        {
            try
            {
                return this._doctorDA.InsertUpdateDoctorResidency(doctorResidency, operation);
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

        public DoctorResidencyResponse GetDoctorResidencyList(int doctorId, int? doctorResidencyId)
        {
            try
            {
                return this._doctorDA.GetDoctorResidencyList(doctorId, doctorResidencyId);
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

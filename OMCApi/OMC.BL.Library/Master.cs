﻿using OMC.BL.Interface;
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

        public List<AddressType> GetAddressTypes(bool? isActive)
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

        public List<RelationshipMaster> GetRelationships(bool? isActive, string relationship, bool? excludeSelf)
        {
            try
            {
                return this._masterDA.GetRelationships(isActive, relationship, excludeSelf);
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

        public List<Gender> GetGenders(bool? isActive, string genderName)
        {
            try
            {
                return this._masterDA.GetGenders(isActive, genderName);
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

        public List<CancerStageMaster> GetCancerStages(bool? isActive, string cancerStageName)
        {
            try
            {
                return this._masterDA.GetCancerStages(isActive, cancerStageName);
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

        public List<SurgeryMaster> GetSurgeryList(bool? isActive, string surgeryName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetSurgeryList(isActive, surgeryName, searchTerm);
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

        public List<IllegalDrugMaster> GetIllegalDrugs(bool? isActive, string IllegalDrug)
        {
            try
            {
                return this._masterDA.GetIllegalDrugs(isActive, IllegalDrug);
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

        public List<AllergyMaster> GetAllergyList(bool? isActive, string allergyName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetAllergyList(isActive, allergyName, searchTerm);
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

        public List<HealthConditionMaster> GetHealthConditionList(bool? isActive, string healthConditionName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetHealthConditionList(isActive, healthConditionName, searchTerm);
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

        public List<DrugTypeMaster> GetDrugTypeList(bool? isActive, string drugType, string searchTerm)
        {
            try
            {
                return this._masterDA.GetDrugTypeList(isActive, drugType, searchTerm);
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

        public List<OccupationMaster> GetOccupationList(bool? isActive, string occupationName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetOccupationList(isActive, occupationName, searchTerm);
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

        public List<DrugSubTypeMaster> GetDrugSubTypeList(int drugTypeId, bool? isActive, string drugSubTypeName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetDrugSubTypeList(drugTypeId, isActive, drugSubTypeName, searchTerm);
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

        public List<DrugBrandMaster> GetDrugBrandList(bool? isActive, string drugBrandName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetDrugBrandList(isActive, drugBrandName, searchTerm);
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

        public List<DrugChemicalMaster> GetDrugChemicalList(bool? isActive, string drugChemicalName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetDrugChemicalList(isActive, drugChemicalName, searchTerm);
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

        public List<DrugFrequencyMaster> GetDrugFrequencyList(bool? isActive, string drugFrequencyName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetDrugFrequencyList(isActive, drugFrequencyName, searchTerm);
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
        
        public List<PackageMaster> GetPackageList(bool? isActive, int? packageId)
        {
            try
            {
                return this._masterDA.GetPackageList(isActive, packageId);
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

        public List<UnitMaster> GetDrugUnitList(bool? isActive, string drugUnitName, string searchTerm)
        {
            try
            {
                return this._masterDA.GetDrugUnitList(isActive, drugUnitName, searchTerm);
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
        public List<MenstrualSymptomsMaster> GetMenstrualSymptoms(bool? isActive, string MenstrualSymptoms)
        {
            try
            {
                return this._masterDA.GetMenstrualSymptoms(isActive, MenstrualSymptoms);
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

        public List<TimezoneMaster> GetTimezones(bool? isActive, string searchTerm)
        {
            try
            {
                return this._masterDA.GetTimezones(isActive, searchTerm);
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

        public List<StateMaster> GetStates(bool? isActive, int? countryId, int? stateId)
        {
            try
            {
                return this._masterDA.GetStates(isActive, countryId, stateId);
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

        public List<BoardMaster> GetBoards(bool? isActive, int? boardId, string board)
        {
            try
            {
                return this._masterDA.GetBoards(isActive, boardId, board);
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

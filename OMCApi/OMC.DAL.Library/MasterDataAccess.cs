﻿using Newtonsoft.Json;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OMC.DAL.Library
{
    public class MasterDataAccess : DataAccessBase, IMasterDataAccess
    {
        #region Declaration
        #endregion

        #region Methods
        public List<Country> GetCountries(bool? isActive)
        {
            try
            {
                Log.Info("Started call to GetCountries");
                Log.Info("parameter values isActive=" + JsonConvert.SerializeObject(new { isActive = isActive }));
                Command.CommandText = "SP_GET_COUNTRIES";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@ACTIVE", isActive);
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<Country> result = new List<Country>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Country
                        {
                            CountryDesc = reader["Country"] != DBNull.Value ? reader["Country"].ToString() : null,
                            Id = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
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

        public List<AddressType> GetAddressTypes(bool? isActive)
        {
            try
            {
                Log.Info("Started call to GetAddressTypes");
                Log.Info("parameter values isActive=" + JsonConvert.SerializeObject(new { isActive = isActive }));
                Command.CommandText = "SP_GET_ADDRESSTYPE";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("@ACTIVE", isActive);
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<AddressType> result = new List<AddressType>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new AddressType
                        {
                            AddressDesc = reader["AddressType"] != DBNull.Value ? reader["AddressType"].ToString() : null,
                            Id = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
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

        public List<Role> GetRoles(bool? isActive, string roleName)
        {
            try
            {
                Log.Info("Started call to GetRoles");
                Log.Info("parameter values isActive=" + JsonConvert.SerializeObject(new { isActive = isActive, roleDescription = roleName }));
                Command.CommandText = "SP_GET_ROLES";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(roleName))
                {
                    Command.Parameters.AddWithValue("@ROLE_NAME", roleName);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<Role> result = new List<Role>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Role
                        {
                            RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
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

        public List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description)
        {
            try
            {
                Log.Info("Started call to GetConsultationStatuses");
                Log.Info("parameter values isActive=" + JsonConvert.SerializeObject(new { isActive = isActive, description = description }));
                Command.CommandText = "SP_GET_CONSULTATION_STATUSES";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(description))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", description);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<ConsultationStatus> result = new List<ConsultationStatus>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new ConsultationStatus
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            SortOrder = Convert.ToInt32(reader["SortOrder"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"].ToString())
                        });
                    }
                }
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

        public List<UserDetail> GetUserList(bool? isActive, string userRole)
        {
            try
            {
                Log.Info("Started call to GetUserList");
                Log.Info("parameter values " + JsonConvert.SerializeObject(new { isActive = isActive, userRole = userRole }));
                Command.CommandText = "SP_GET_USERS";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(userRole))
                {
                    Command.Parameters.AddWithValue("@USER_ROLE", userRole);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<UserDetail> result = new List<UserDetail>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new UserDetail
                        {
                            FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                            LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString())
                        });
                    }
                }
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

        public List<RelationshipMaster> GetRelationships(bool? isActive, string relationship, bool? excludeSelf)
        {
            try
            {
                Log.Info("Started call to GetRelationships");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, relationship = relationship }));
                Command.CommandText = "SP_GET_RELATIONSHIPS";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(relationship))
                {
                    Command.Parameters.AddWithValue("@RELATIONSHIP_NAME", relationship);
                }
                if (excludeSelf.HasValue)
                {
                    Command.Parameters.AddWithValue("@EXCLUDE_SELF", excludeSelf);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<RelationshipMaster> result = new List<RelationshipMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new RelationshipMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetRelationships result " + JsonConvert.SerializeObject(result));
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

        public List<Gender> GetGenders(bool? isActive, string genderName)
        {
            try
            {
                Log.Info("Started call to GetGenders");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, genderName = genderName }));
                Command.CommandText = "SP_GET_GENDERS";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(genderName))
                {
                    Command.Parameters.AddWithValue("@GENDER_NAME", genderName );
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<Gender> result = new List<Gender>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Gender
                        {
                            GenderName = reader["GenderName"] != DBNull.Value ? reader["GenderName"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetGenders result " + JsonConvert.SerializeObject(result));
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

        public List<CancerStageMaster> GetCancerStages(bool? isActive, string cancerStageName)
        {
            try
            {
                Log.Info("Started call to GetCancerStages");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, cancerStageName = cancerStageName }));
                Command.CommandText = "SP_GET_CANCER_STAGE_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(cancerStageName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", cancerStageName);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<CancerStageMaster> result = new List<CancerStageMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new CancerStageMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetCancerStages result " + JsonConvert.SerializeObject(result));
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

        public List<SurgeryMaster> GetSurgeryList(bool? isActive, string surgeryName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetSurgeryList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, surgeryName = surgeryName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_SURGERY_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(surgeryName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", surgeryName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<SurgeryMaster> result = new List<SurgeryMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new SurgeryMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetSurgeryList result " + JsonConvert.SerializeObject(result));
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

        public List<IllegalDrugMaster> GetIllegalDrugs(bool? isActive, string IllegalDrug)
        {
            try
            {
                Log.Info("Started call to GetIllegalDrugs");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, IllegalDrug = IllegalDrug }));
                Command.CommandText = "SP_GET_ILLEGALDRUGS_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(IllegalDrug))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", IllegalDrug);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<IllegalDrugMaster> result = new List<IllegalDrugMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new IllegalDrugMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            ID = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetIllegalDrugs result " + JsonConvert.SerializeObject(result));
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

        public List<AllergyMaster> GetAllergyList(bool? isActive, string allergyName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetAllergyList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, allergyName = allergyName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_ALLERGY_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(allergyName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", allergyName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<AllergyMaster> result = new List<AllergyMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new AllergyMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetAllergyList result " + JsonConvert.SerializeObject(result));
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

        public List<HealthConditionMaster> GetHealthConditionList(bool? isActive, string healthConditionName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetHealthConditionList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, healthConditionName = healthConditionName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_HEALTH_CONDITION_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(healthConditionName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", healthConditionName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<HealthConditionMaster> result = new List<HealthConditionMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new HealthConditionMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetHealthConditionList result " + JsonConvert.SerializeObject(result));
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

        public List<OccupationMaster> GetOccupationList(bool? isActive, string occupationName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetOccupationList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, occupationName = occupationName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_OCCUPATION_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(occupationName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", occupationName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<OccupationMaster> result = new List<OccupationMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new OccupationMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetOccupationList result " + JsonConvert.SerializeObject(result));
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

        public List<PackageMaster> GetPackageList(bool? isActive, int? packageId)
        {
            try
            {
                Log.Info("Started call to GetPackageList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, packageId = packageId }));
                Command.CommandText = "SP_GET_PACKAGE_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (packageId.HasValue)
                {
                    Command.Parameters.AddWithValue("@PACKAGE_ID", packageId);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<PackageMaster> result = new List<PackageMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new PackageMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Packagename = reader["Packagename"] != DBNull.Value ? reader["Packagename"].ToString() : null,
                            Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"].ToString()) : 0
                        });
                    }
                }
                Log.Info("End call to GetPackageList result " + JsonConvert.SerializeObject(result));
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

        public List<DrugTypeMaster> GetDrugTypeList(bool? isActive, string drugType, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetDrugTypeList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, drugType = drugType, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_DRUG_TYPE_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(drugType))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", drugType);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<DrugTypeMaster> result = new List<DrugTypeMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DrugTypeMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetDrugTypeList result " + JsonConvert.SerializeObject(result));
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

        public List<DrugSubTypeMaster> GetDrugSubTypeList(int drugTypeId, bool? isActive, string drugSubTypeName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetDrugSubTypeList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new
                {
                    drugTypeId = drugTypeId,
                    isActive = isActive,
                    drugSubTypeName = drugSubTypeName,
                    searchTerm = searchTerm
                }));
                Command.CommandText = "SP_GET_DRUG_SUB_TYPE_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(drugSubTypeName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", drugSubTypeName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);
                Command.Parameters.AddWithValue("@DRUG_TYPE_ID", drugTypeId);
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<DrugSubTypeMaster> result = new List<DrugSubTypeMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DrugSubTypeMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            DrugTypeId = Convert.ToInt32(reader["DrugTypeId"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetDrugSubTypeList result " + JsonConvert.SerializeObject(result));
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

        public List<DrugBrandMaster> GetDrugBrandList(bool? isActive, string drugBrandName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetDrugBrandList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, drugBrandName = drugBrandName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_DRUG_BRAND_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(drugBrandName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", drugBrandName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<DrugBrandMaster> result = new List<DrugBrandMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DrugBrandMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetDrugBrandList result " + JsonConvert.SerializeObject(result));
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

        public List<DrugChemicalMaster> GetDrugChemicalList(bool? isActive, string drugChemicalName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetDrugChemicalList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, drugChemicalName = drugChemicalName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_DRUG_CHEMICAL_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(drugChemicalName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", drugChemicalName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<DrugChemicalMaster> result = new List<DrugChemicalMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DrugChemicalMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetDrugChemicalList result " + JsonConvert.SerializeObject(result));
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

        public List<DrugFrequencyMaster> GetDrugFrequencyList(bool? isActive, string drugFrequencyName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetDrugFrequencyList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, drugFrequencyName = drugFrequencyName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_DRUG_FREQUENCY_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(drugFrequencyName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", drugFrequencyName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<DrugFrequencyMaster> result = new List<DrugFrequencyMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DrugFrequencyMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetDrugFrequencyList result " + JsonConvert.SerializeObject(result));
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

        public List<UnitMaster> GetDrugUnitList(bool? isActive, string drugUnitName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetDrugUnitList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, drugUnitName = drugUnitName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_UNIT_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(drugUnitName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", drugUnitName);
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<UnitMaster> result = new List<UnitMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new UnitMaster
                        {
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetDrugUnitList result " + JsonConvert.SerializeObject(result));
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

        public List<MenstrualSymptomsMaster> GetMenstrualSymptoms(bool? isActive, string MenstrualSymptoms)
        {
            try
            {
                Log.Info("Started call to GetMenstrualSymptoms");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, MenstrualSymptoms = MenstrualSymptoms }));
                Command.CommandText = "SP_GET_MENSTRUALSYMPTOMS_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(MenstrualSymptoms))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", MenstrualSymptoms);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<MenstrualSymptomsMaster> result = new List<MenstrualSymptomsMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new MenstrualSymptomsMaster
                        {
                            SymptomDescription = reader["SymptomDescription"] != DBNull.Value ? reader["SymptomDescription"].ToString() : null,
                            Id = Convert.ToInt32(reader["ID"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetMenstrualSymptoms result " + JsonConvert.SerializeObject(result));
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

        public List<TimezoneMaster> GetTimezones(bool? isActive, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetTimezones");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_TIMEZONES";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();                
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Command.Parameters.AddWithValue("@SEARCH_TERM", searchTerm);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<TimezoneMaster> result = new List<TimezoneMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new TimezoneMaster
                        {
                            ShortForm = reader["ShortForm"] != DBNull.Value ? reader["ShortForm"].ToString() : null,
                            Time = reader["Time"] != DBNull.Value ? reader["Time"].ToString() : null,
                            Timezone = reader["Timezone"] != DBNull.Value ? reader["Timezone"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetTimezones result " + JsonConvert.SerializeObject(result));
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

        public List<StateMaster> GetStates(bool? isActive, int? countryId, int? stateId)
        {
            try
            {
                Log.Info("Started call to GetStates");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, countryId = countryId, stateId = stateId }));
                Command.CommandText = "SP_GET_STATES";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (countryId.HasValue)
                {
                    Command.Parameters.AddWithValue("@COUNTRY_ID", countryId);
                }
                if (stateId.HasValue)
                {
                    Command.Parameters.AddWithValue("@STATE_ID", stateId);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<StateMaster> result = new List<StateMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new StateMaster
                        {
                            CountryId = int.Parse(reader["CountryId"].ToString()),
                            CountryName = reader["CountryName"] != DBNull.Value ? reader["CountryName"].ToString() : null,
                            State = reader["State"] != DBNull.Value ? reader["State"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetStates result " + JsonConvert.SerializeObject(result));
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

        public List<BoardMaster> GetBoards(bool? isActive, int? boardId, string board)
        {
            try
            {
                Log.Info("Started call to GetBoards");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, boardId = boardId, board = board }));
                Command.CommandText = "SP_GET_BOARDS";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (boardId.HasValue)
                {
                    Command.Parameters.AddWithValue("@BOARD_ID", boardId);
                }
                if (!string.IsNullOrEmpty(board))
                {
                    Command.Parameters.AddWithValue("@BOARD", board);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<BoardMaster> result = new List<BoardMaster>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new BoardMaster
                        {
                            Board = reader["Board"] != DBNull.Value ? reader["Board"].ToString() : null,
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                Log.Info("End call to GetBoards result " + JsonConvert.SerializeObject(result));
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

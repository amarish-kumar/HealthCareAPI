using Newtonsoft.Json;
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

        public List<HealthConditionMaster> GetHealthConditionList(bool? isActive, string healthConditionNameName, string searchTerm)
        {
            try
            {
                Log.Info("Started call to GetHealthConditionList");
                Log.Info("parameter values =" + JsonConvert.SerializeObject(new { isActive = isActive, healthConditionNameName = healthConditionNameName, searchTerm = searchTerm }));
                Command.CommandText = "SP_GET_HEALTH_CONDITION_MASTER";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(healthConditionNameName))
                {
                    Command.Parameters.AddWithValue("@DESCRIPTION", healthConditionNameName);
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
        #endregion
    }
}

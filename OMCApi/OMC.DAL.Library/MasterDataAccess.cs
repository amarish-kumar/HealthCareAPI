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
                            ID = Convert.ToInt32(reader["ID"].ToString())
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

        public List<Address> GetAddressTypes(bool? isActive)
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
                List<Address> result = new List<Address>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Address
                        {
                            AddressDesc = reader["AddressType"] != DBNull.Value ? reader["AddressType"].ToString() : null,
                            ID = Convert.ToInt32(reader["ID"].ToString())
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
        #endregion
    }
}
